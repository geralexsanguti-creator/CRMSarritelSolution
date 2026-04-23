using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CRMSarritelApi.Services
{
    public class CommissionService : ICommissionService
    {
        private readonly CRMSarritelDbContext _context;
        private readonly Microsoft.Extensions.Logging.ILogger<CommissionService> _logger;

        public CommissionService(CRMSarritelDbContext context, Microsoft.Extensions.Logging.ILogger<CommissionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CalculateCommissionsForSale(int ventaId, bool ignoreManual = false)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            
            await strategy.ExecuteAsync(async () => {
                using var transaction = await _context.Database.BeginTransactionAsync();
                
                // Extraemos el objeto Venta para la retroactividad fuera del try-catch de la transacción
                Venta? ventaForRetroactive = null;
                
                try
                {
                    _logger.LogInformation("Calculando comisiones para venta ID: {VentaId}", ventaId);
                    
                    var venta = await _context.Ventas
                        .Include(v => v.TipoVenta)
                        .Include(v => v.Detalles).ThenInclude(d => d.Producto)
                            .ThenInclude(p => p!.ProductoCarpetas)
                                .ThenInclude(pc => pc.CarpetaReglas)
                                    .ThenInclude(c => c!.CarpetaReglasComision)
                        .Include(v => v.Usuario)
                            .ThenInclude(u => u!.UsuarioRoles)
                                .ThenInclude(ur => ur.Rol)
                        .Include(v => v.Estado)
                        .FirstOrDefaultAsync(v => v.Id == ventaId);

                    if (venta == null) return;
                    
                    ventaForRetroactive = venta;

                    await EnsureSystemUserAsync();

                    bool esVentaGanada = venta.Estado?.EsGanada ?? false;
                    _logger.LogInformation(">>> [COM] Procesando Venta #{Id} - Vendedor: {Nom} - Estado: {Est}", venta.Id, venta.Usuario?.Nombre, venta.Estado?.Nombre);

                    if (venta.TipoVenta != null && !string.IsNullOrEmpty(venta.TipoVenta.EstadosVentaJson))
                    {
                        try {
                            var defs = JsonSerializer.Deserialize<List<EstadoDef>>(venta.TipoVenta.EstadosVentaJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            var match = defs?.FirstOrDefault(d => string.Equals(d.Codigo, venta.Estado?.Codigo, StringComparison.OrdinalIgnoreCase));
                            if (match != null && match.EsGanada) 
                            {
                                _logger.LogInformation("Estado coincidente en JSON '{Codigo}' tiene EsGanada=true. Activando.", match.Codigo);
                                esVentaGanada = true;
                            }
                        } catch (Exception ex) { 
                            _logger.LogError(ex, "Error al parsear EstadosVentaJson para Venta {VentaId}", ventaId);
                        }
                    }

                    // Heurística de respaldo: Si el nombre o código contiene palabras clave de éxito, activar.
                    if (!esVentaGanada && venta.Estado != null)
                    {
                        var name = (venta.Estado.Nombre ?? "").ToUpper();
                        var code = (venta.Estado.Codigo ?? "").ToUpper();
                        if (name.Contains("ACTIVO") || name.Contains("GANADA") || name.Contains("CERRADA") || 
                            code.Contains("ACTI") || code.Contains("GANA") || code.Contains("CERR") || code.Contains("WON"))
                        {
                            _logger.LogInformation("Estado '{Nombre}' detectado como exitoso por heurística. Activando comisión.", venta.Estado.Nombre);
                            esVentaGanada = true;
                        }
                    }

                    // Comisiones son proyectadas (inactivas) desde que se crea la venta,
                    // y al cerrarse (EsGanada) pasan a ser activas.
                    var allExisting = await _context.Comisiones.Where(c => c.VentaId == ventaId).ToListAsync();
                    
                    var detallesIdsActuales = venta.Detalles.Select(d => d.Id).ToHashSet();
                    
                    // Si ignoreManual es true, borramos ABSOLUTAMENTE TODO lo que no sea 'PAGADA'.
                    // Si es false, respetamos MANUAL y EXTRAIDA.
                    var toKeep = allExisting.Where(c => 
                        (c.Estado.Codigo == "PAGA") ||
                        (!ignoreManual && (c.Tipo.Codigo == "MANUAL" || c.Tipo.Codigo == "EXTRAIDA"))
                    ).Where(c => !c.DetalleVentaId.HasValue || detallesIdsActuales.Contains(c.DetalleVentaId.Value))
                    .ToList();

                    var toRemove = allExisting.Except(toKeep).ToList();

                    if (toRemove.Any()) {
                        _context.Comisiones.RemoveRange(toRemove);
                        await _context.SaveChangesAsync();
                    }

                    // Actualizar el estado de las comisiones preservadas
                    foreach (var manual in toKeep)
                    {
                        if (manual.Estado.Codigo == "PAGA") continue;
                        var targetEstado = esVentaGanada ? EstadoComisionValue.Activa : EstadoComisionValue.Inactiva;
                        if (manual.Estado.Codigo != targetEstado.Codigo)
                        {
                            manual.Estado = targetEstado;
                            _context.Comisiones.Update(manual);
                        }
                    }
                    if (toKeep.Any()) await _context.SaveChangesAsync();

                    // Identificar qué beneficiarios ya tienen comisiones preservadas (manuales/pagadas) 
                    // para evitar duplicarlos durante el cálculo automático.
                    var alreadyHandled = toKeep
                        .Where(c => c.DetalleVentaId.HasValue)
                        .Select(c => new { c.DetalleVentaId, c.EmpleadoId, c.AppliedTierId })
                        .ToHashSet();

                    _logger.LogInformation("    [COM] Venta #{Id} tiene {Count} detalles.", venta.Id, venta.Detalles.Count);
                    if (!venta.Detalles.Any()) _logger.LogWarning("    [WARN] Venta #{Id} no tiene detalles de producto.", venta.Id);

                    foreach (var detalle in venta.Detalles)
                    {
                        if (detalle.Producto == null) {
                            _logger.LogWarning("    [WARN] Detalle {DetId} no tiene producto asociado.", detalle.Id);
                            continue;
                        }

                        // Si el producto ha sido ajustado manualmente, no aplicamos reglas automáticas
                        // para evitar duplicidades o descuadres presupuestarios (como que vuelvan a aparecer eliminadas).
                        if (!ignoreManual && allExisting.Any(c => c.DetalleVentaId == detalle.Id && (c.Tipo.Codigo == "MANUAL" || c.Tipo.Codigo == "EXTRAIDA")))
                        {
                            _logger.LogInformation("    [SKIP] Producto {Id} tiene ajustes manuales. Saltando reglas automáticas.", detalle.Id);
                            continue;
                        }

                        var reglaIds = (detalle.Producto.ProductoCarpetas ?? new List<ProductoCarpeta>())
                            .Where(pc => pc.CarpetaReglas != null && pc.CarpetaReglas.Activo)
                            .SelectMany(pc => pc.CarpetaReglas?.CarpetaReglasComision ?? new List<CarpetaReglaComision>())
                            .Select(crc => crc.ReglaComisionId)
                            .Distinct().ToList();

                        _logger.LogInformation("    [PROD] Detalle {DetId} ({Prod}): {Count} reglas encontradas.", 
                            detalle.Id, detalle.Producto.Nombre, reglaIds.Count);

                        if (!reglaIds.Any()) {
                            _logger.LogWarning("    [WARN] El producto '{Prod}' no tiene reglas asignadas o activas.", detalle.Producto.Nombre);
                            continue;
                        }

                        var reglas = await _context.ReglasComisiones
                            .Include(r => r.ReparticionesBase).ThenInclude(rb => rb.Rol)
                            .Include(r => r.Tiers).ThenInclude(t => t.Reparticiones).ThenInclude(tr => tr.Rol)
                            .Where(r => reglaIds.Contains(r.Id) && r.Activa)
                            .ToListAsync();

                        foreach (var regla in reglas)
                        {
                            await ProcessRule(venta, detalle, regla, esVentaGanada, toKeep, ignoreManual);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    _logger.LogInformation("Comisiones finalizadas para Venta {VentaId}.", ventaId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Fallo en el cálculo de comisión para Venta {VentaId}", ventaId);
                    throw;
                }
                
                // NOTA: El usuario ha solicitado explícitamente que NO HAYA RETROACTIVIDAD AUTOMÁTICA.
                // Las ventas pasadas (1-5) se quedan con su Tier original.
                // Solo la venta actual (ej. la 6 o 7) toma el nuevo Tier.
                // Por lo tanto, no recalculamos a los hermanos automáticamente aquí.
            });
        }

        private class EstadoDef
        {
            public string? Codigo { get; set; }
            public bool EsGanada { get; set; }
        }

        private async Task ProcessRule(Venta venta, DetalleVenta detalle, ReglaComision regla, bool esVentaGanada, List<Comision> toKeep, bool ignoreManual = false)
        {
            _logger.LogInformation("    [RULE] Aplicando regla '{Regla}'", regla.Nombre);
            DateTime fechaAncla = venta.FechaInstalacionReal ?? venta.FechaVenta;
            decimal volumenActual = await CalculateVolumeForPeriod(venta.UsuarioId, regla.Variable, fechaAncla);
            
            // Obtenemos los roles del vendedor para priorizar su tier
            var sellerRoles = venta.Usuario?.UsuarioRoles?.Select(ur => ur.RolId).ToList() ?? new List<int>();
            if (!sellerRoles.Any() && venta.UsuarioId != 0)
            {
                sellerRoles = await _context.UsuarioRoles
                    .Where(ur => ur.UsuarioId == venta.UsuarioId)
                    .Select(ur => ur.RolId)
                    .ToListAsync();
            }

            // B. Seleccionar el UNICO Tier Activo
            // Priorizamos los tiers que tengan una repartición explícita para el rol del vendedor
            var validTiers = (regla.Tiers ?? new List<ReglaComisionTier>())
                .Where(t => volumenActual >= t.ValorMin && (t.ValorMax == null || t.ValorMax == 0 || volumenActual <= t.ValorMax))
                .ToList();

            var activeTier = validTiers
                .Where(t => t.Reparticiones != null && t.Reparticiones.Any(r => r.RolId.HasValue && sellerRoles.Contains(r.RolId.Value)))
                .OrderByDescending(t => t.ValorMin)
                .FirstOrDefault() 
                ?? validTiers.OrderByDescending(t => t.ValorMin).FirstOrDefault();

            // Si el volumen excede todos los tiers configurados, usar el último tier (el más alto)
            if (activeTier == null && regla.Tiers != null && regla.Tiers.Any())
            {
                var overflowTiers = regla.Tiers.OrderByDescending(t => t.ValorMax ?? t.ValorMin).ToList();
                activeTier = overflowTiers
                    .Where(t => t.Reparticiones != null && t.Reparticiones.Any(r => r.RolId.HasValue && sellerRoles.Contains(r.RolId.Value)))
                    .FirstOrDefault()
                    ?? overflowTiers.FirstOrDefault();
            }

            _logger.LogInformation("    [RULE] {Regla} (ID {Rid}) - Vol: {Vol} -> Tier: {Tier} (ID {Tid})", 
                regla.Nombre, regla.Id, volumenActual, activeTier?.Nombre ?? "NINGUNO", activeTier?.Id ?? 0);

            // El umbral es el ValorMin del primer Tier que tiene un split para EL VENDEDOR (basado en sus roles)
            var roleSpecificThreshold = (regla.Tiers ?? new List<ReglaComisionTier>())
                .Where(t => t.Reparticiones != null && t.Reparticiones.Any(rep => rep.RolId.HasValue && sellerRoles.Contains(rep.RolId.Value)))
                .Select(t => t.ValorMin)
                .DefaultIfEmpty(0)
                .Min();

            _logger.LogInformation("    [STRICT] Umbral Mínimo para el Rol del Vendedor: {Min}", roleSpecificThreshold);

            // Si el volumen actual no llega al mínimo para que el VENDEDOR empiece a cobrar,
            // bloqueamos TODO el reparto (incluido managers) para esta regla en este tier.
            if (volumenActual < roleSpecificThreshold)
            {
                _logger.LogWarning("    [STRICT] Volumen {Vol} inferior al umbral del vendedor {Min}. Bloqueando reparto total.", volumenActual, roleSpecificThreshold);
                decimal grossBase = regla.TipoRemuneracionGross == "fijo" ? regla.ValorRemuneracionGross * detalle.Cantidad : (detalle.Total * (regla.ValorRemuneracionGross / 100));
                if (grossBase > 0)
                {
                    string strictNote = $"[BLOQUEO ROL - Vol: {volumenActual}] No alcanza umbral del vendedor ({roleSpecificThreshold})";
                    await AddComisionRecord(venta, detalle, regla, null, 99, grossBase, grossBase, null, esVentaGanada, strictNote);
                }
                return;
            }

            // D. Determinar Gross y Reparticiones
            string tipoGross = activeTier?.TipoRemuneracionGross ?? regla.TipoRemuneracionGross ?? "fijo";
            decimal valorGross = (activeTier != null && activeTier.ValorRemuneracionGross.HasValue && activeTier.ValorRemuneracionGross.Value > 0) 
                ? activeTier.ValorRemuneracionGross.Value 
                : regla.ValorRemuneracionGross;
            decimal grossMonto = tipoGross == "fijo" ? valorGross * detalle.Cantidad : (detalle.Total * (valorGross / 100));

            _logger.LogInformation("    [GROSS] Tipo: {Tipo}, Valor: {Val}, Cant: {Cant}, Total Det: {Tot} -> Bruto: {Gross}", 
                tipoGross, valorGross, detalle.Cantidad, detalle.Total, grossMonto);

            if (grossMonto <= 0) {
                _logger.LogWarning("    [SKIP] Bruto calculado es 0 para la regla {Regla}. Verifique configuración.", regla.Nombre);
                return;
            }

            // D. Combinar Reparticiones (Herencia Base + Tier)
            var splits = (regla.ReparticionesBase ?? new List<ReparticionComision>()).ToList();
            _logger.LogInformation("    [RULE] Base Splits: {Count}", splits.Count);

            if (activeTier != null && activeTier.Reparticiones != null)
            {
                _logger.LogInformation("    [RULE] Tier Splits: {Count}", activeTier.Reparticiones.Count);
                foreach (var tierRep in activeTier.Reparticiones)
                {
                    // Cargar roles si faltan para poder comparar correctamente
                    if (tierRep.RolId.HasValue && tierRep.Rol == null) {
                        tierRep.Rol = await _context.Roles.FindAsync(tierRep.RolId.Value);
                    }

                    bool isTierSeller = tierRep.RolId.HasValue && (sellerRoles.Contains(tierRep.RolId.Value) || 
                                     (tierRep.Rol?.Nombre ?? "").ToUpper().Contains("VENDEDOR") || 
                                     (tierRep.Rol?.Nombre ?? "").ToUpper().Contains("COMERCIAL") ||
                                     (tierRep.Rol?.Nombre ?? "").ToUpper().Contains("AGENTE"));

                    int existingIdx = splits.FindIndex(s => 
                    {
                        if (s.RolId.HasValue && s.Rol == null) {
                            s.Rol = _context.Roles.Find(s.RolId.Value); // Sync find is okay here since it's cached or fast
                        }
                        bool isBaseSeller = s.RolId.HasValue && (sellerRoles.Contains(s.RolId.Value) || 
                                     (s.Rol?.Nombre ?? "").ToUpper().Contains("VENDEDOR") || 
                                     (s.Rol?.Nombre ?? "").ToUpper().Contains("COMERCIAL") ||
                                     (s.Rol?.Nombre ?? "").ToUpper().Contains("AGENTE"));

                        return (s.RolId.HasValue && s.RolId == tierRep.RolId) ||
                               (s.EquipoId.HasValue && s.EquipoId == tierRep.EquipoId) ||
                               (s.UsuarioId.HasValue && s.UsuarioId == tierRep.UsuarioId) ||
                               (isTierSeller && isBaseSeller); // Tier seller OVERRIDES Base seller
                    });

                    if (existingIdx >= 0) splits[existingIdx] = tierRep;
                    else splits.Add(tierRep);
                }
            }

            _logger.LogInformation("    [RULE] Total Splits Finales: {Count}", splits.Count);
            decimal totalDistributedAmount = 0;
            
            // Cargar roles del vendedor si no están (para evitar fallos de Lazy Loading/Includes)
            sellerRoles = venta.Usuario?.UsuarioRoles?.Select(ur => ur.RolId).ToList() ?? new List<int>();
            if (!sellerRoles.Any() && venta.UsuarioId != 0)
            {
                sellerRoles = await _context.UsuarioRoles
                    .Where(ur => ur.UsuarioId == venta.UsuarioId)
                    .Select(ur => ur.RolId)
                    .ToListAsync();
            }

            _logger.LogInformation("    [DATA] Seller Roles: {Roles}", string.Join(",", sellerRoles));
            bool sellerAlreadyCommisioned = false;

            // E. Procesar Splits
            foreach (var rep in splits)
            {
                int? beneficiaryId = null;
                string notes = "Comisión Automática";

                if (rep.RolId.HasValue)
                {
                    if (rep.Rol == null) {
                        rep.Rol = await _context.Roles.FindAsync(rep.RolId.Value);
                    }

                    string rolNombre = (rep.Rol?.Nombre ?? "Rol Desconocido").ToUpper();
                    _logger.LogInformation("    [SPLIT] Analizando Rol ID {RolId} ({Nom}) - Split {Val}", rep.RolId, rolNombre, rep.Valor);
                    
                    // A. ¿Es el vendedor?
                    bool esVendedor = sellerRoles.Contains(rep.RolId.Value) || 
                                     rolNombre.Contains("VENDEDOR") || 
                                     rolNombre.Contains("COMERCIAL") ||
                                     rolNombre.Contains("AGENTE");

                    if (esVendedor)
                    {
                        if (sellerAlreadyCommisioned) {
                            _logger.LogInformation("    [SKIP] Vendedor ya tiene una comisión de rol.");
                            continue;
                        }
                        beneficiaryId = venta.UsuarioId;
                        notes = $"Vendedor ({rolNombre})";
                        sellerAlreadyCommisioned = true;
                    }
                    else
                    {
                        // B. ¿Es un superior/manager?
                        beneficiaryId = await FindBeneficiaryByRole(venta.UsuarioId, rep.RolId.Value);
                        
                        // C. Fallback semántico para Manager
                        if (!beneficiaryId.HasValue && (rolNombre.Contains("MANAGER") || rolNombre.Contains("EQUIPO") || rolNombre.Contains("SUPERVISOR") || rolNombre.Contains("JEFE")))
                        {
                            var teams = await _context.UsuarioEquipos.Where(ue => ue.UsuarioId == venta.UsuarioId).Select(ue => ue.EquipoId).ToListAsync();
                            beneficiaryId = await _context.UsuarioEquipos
                                .Where(ue => teams.Contains(ue.EquipoId) && ue.EsManager)
                                .Select(ue => ue.UsuarioId)
                                .FirstOrDefaultAsync();
                            if (beneficiaryId == 0) beneficiaryId = null;

                            // D. Último recurso: Si es un rol de gestión y no hay manager, va para el Administrador (ID 1)
                            if (!beneficiaryId.HasValue) {
                                beneficiaryId = 1; 
                                notes = "Gestión (Asignado a Admin)";
                            } else {
                                notes = "Manager (Fallback Semántico)";
                            }
                        }

                        if (beneficiaryId.HasValue && string.IsNullOrEmpty(notes)) notes = "Jerarquía (" + rolNombre + ")";
                    }
                }
                else if (rep.EquipoId.HasValue)
                {
                    var managers = await _context.UsuarioEquipos
                        .Where(ue => ue.EquipoId == rep.EquipoId.Value && ue.EsManager)
                        .Select(ue => ue.UsuarioId)
                        .ToListAsync();

                    _logger.LogInformation("    [SPLIT] Equipo ID {EqId} -> {Count} managers encontrados", rep.EquipoId, managers.Count);

                    if (managers.Any())
                    {
                        decimal totalSplit = rep.TipoCalculo == "fijo" ? rep.Valor * detalle.Cantidad : grossMonto * (rep.Valor / 100);
                        decimal individualAmount = totalSplit / managers.Count;
                        foreach (var mId in managers)
                        {
                            totalDistributedAmount += await SaveComisionOrSkipManual(venta, detalle, regla, activeTier, mId, individualAmount, grossMonto, rep, esVentaGanada, "Gestión Equipo", toKeep, volumenActual, ignoreManual);
                        }
                        continue;
                    }
                }
                else if (rep.UsuarioId.HasValue)
                {
                    beneficiaryId = rep.UsuarioId.Value;
                    notes = "Asignación Directa";
                }

                if (beneficiaryId.HasValue)
                {
                    decimal amount = rep.TipoCalculo == "fijo" ? rep.Valor * detalle.Cantidad : grossMonto * (rep.Valor / 100);
                    _logger.LogInformation("    [MATCH] Beneficiario ID {Id}: {Monto}", beneficiaryId, amount);
                    totalDistributedAmount += await SaveComisionOrSkipManual(venta, detalle, regla, activeTier, beneficiaryId.Value, amount, grossMonto, rep, esVentaGanada, notes, toKeep, volumenActual, ignoreManual);
                }
            }

            // F. Beneficio SISTEMA (Remanente)
            decimal remanente = Math.Max(0, grossMonto - totalDistributedAmount);
            _logger.LogInformation("    [FINAL] Bruto: {Gross}, Distribuido: {Dist}, Remanente: {Rem}", grossMonto, totalDistributedAmount, remanente);
            
            if (remanente > 0.01m)
            {
                // Solo respetamos si hay un ajuste MANUAL previo
                var manualSystem = toKeep.FirstOrDefault(c => c.EmpleadoId == 99 && c.DetalleVentaId == detalle.Id && (c.Tipo.Codigo == "MANUAL" || c.Tipo.Codigo == "EXTRAIDA"));
                
                if (manualSystem == null)
                {
                    string systemNote = $"[BASE - Vol: {volumenActual}] Beneficio Neto Organización";
                    if (activeTier != null) systemNote = $"[Tier: {activeTier.Nombre} - Vol: {volumenActual}] Beneficio Neto Organización";
                    
                    await AddComisionRecord(venta, detalle, regla, activeTier, 99, remanente, grossMonto, null, esVentaGanada, systemNote);
                }
            }
        }

        private async Task EnsureSystemUserAsync()
        {
            try 
            {
                var systemUser = await _context.Usuarios.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Id == 99);
                if (systemUser == null)
                {
                    _logger.LogInformation("[SISTEMA] Creando usuario virtual de Organización (ID 99)");
                    systemUser = new Usuario
                    {
                        Id = 99,
                        Nombre = "SISTEMA (Organización)",
                        Username = "sistema",
                        Email = "sistema@sarritel.com",
                        PasswordHash = "SYSTEM_VIRTUAL_USER",
                        Activo = true,
                        FechaCreation = DateTime.UtcNow
                    };
                    
                    _context.Usuarios.Add(systemUser);
                    // En PostgreSQL no se usa IDENTITY_INSERT, simplemente se inserta el ID.
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("[SISTEMA] Usuario 99 creado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("[SISTEMA] No se pudo crear/verificar el usuario 99: {Msg}. Es posible que ya exista o la DB restrinja IDs manuales.", ex.Message);
            }
        }

        private async Task<decimal> SaveComisionOrSkipManual(Venta v, DetalleVenta d, ReglaComision r, ReglaComisionTier? t, int empId, decimal monto, decimal gross, ReparticionComision? rep, bool ganada, string notas, List<Comision> existing, decimal volumen, bool ignoreManual = false)
        {
            if (!ignoreManual)
            {
                var manual = existing.FirstOrDefault(c => c.EmpleadoId == empId && c.DetalleVentaId == d.Id && (c.Tipo.Codigo == "MANUAL" || c.Tipo.Codigo == "EXTRAIDA"));
                if (manual != null)
                {
                    _logger.LogInformation("    [MANUAL] Respetando ajuste para Usuario {Id}: {Monto}", empId, manual.MontoComision);
                    
                    if (!(manual.Notas ?? "").Contains("[AJUSTE MANUAL]")) {
                        manual.Notas = "[AJUSTE MANUAL] " + (manual.Notas ?? "");
                        _context.Comisiones.Update(manual);
                    }
                    
                    return manual.MontoComision;
                }
            }

            // Nota técnica para transparencia en el panel
            string techNote = notas;
            if (t != null) techNote = $"[Tier: {t.Nombre} (Min: {t.ValorMin}, Max: {t.ValorMax}) - Vol: {volumen}] {notas}";
            else techNote = $"[BASE - Vol: {volumen}] {notas}";

            await AddComisionRecord(v, d, r, t, empId, monto, gross, rep, ganada, techNote);
            return monto;
        }

        private async Task<int?> FindBeneficiaryByRole(int sellerId, int roleId)
        {
            _logger.LogInformation("        [TEAM] Buscando Manager para el vendedor {SellerId} con Rol objetivo {RolId}", sellerId, roleId);
            
            // 1. Obtener equipos del vendedor
            var teamIds = await _context.UsuarioEquipos
                .Where(ue => ue.UsuarioId == sellerId)
                .Select(ue => ue.EquipoId)
                .ToListAsync();

            if (teamIds.Any())
            {
                // 2. Buscar al manager de esos equipos
                var managers = await _context.UsuarioEquipos
                    .Include(ue => ue.Usuario).ThenInclude(u => u.UsuarioRoles)
                    .Where(ue => teamIds.Contains(ue.EquipoId) && ue.EsManager && ue.Usuario.Activo)
                    .ToListAsync();

                // Intentar match por RolId primero
                var perfectMatch = managers.FirstOrDefault(ue => ue.Usuario != null && 
                                                                ue.Usuario.UsuarioRoles != null && 
                                                                ue.Usuario.UsuarioRoles.Any(ur => ur.RolId == roleId));
                if (perfectMatch != null) {
                    _logger.LogInformation("        [TEAM] Manager con Rol exacto encontrado: {Id}", perfectMatch.UsuarioId);
                    return perfectMatch.UsuarioId;
                }

                // Si no hay match exacto, pero hay managers, devolver el primero
                var fallbackManager = managers.FirstOrDefault(ue => ue.Usuario != null && ue.Usuario.Activo);
                if (fallbackManager != null) {
                    _logger.LogInformation("        [TEAM] Manager de equipo encontrado (fallback de rol): {Id}", fallbackManager.UsuarioId);
                    return fallbackManager.UsuarioId;
                }
            }

            // 3. Global Fallback (solo si no hay equipo o manager)
            var globalBeneficiary = await _context.UsuarioRoles
                .Include(ur => ur.Usuario)
                .Where(ur => ur.RolId == roleId && ur.Usuario.Activo)
                .Select(ur => ur.UsuarioId)
                .FirstOrDefaultAsync();

            if (globalBeneficiary != 0) {
                _logger.LogInformation("        [GLOBAL] Beneficiario por rol encontrado fuera de equipo: {Id}", globalBeneficiary);
                return globalBeneficiary;
            }
            
            return null;
        }

        private async Task AddComisionRecord(Venta venta, DetalleVenta detalle, ReglaComision regla, ReglaComisionTier? tier, int beneficiarioId, decimal monto, decimal baseCalculo, ReparticionComision? rep, bool esVentaGanada, string notas)
        {
            var com = new Comision
            {
                VentaId = venta.Id,
                DetalleVentaId = detalle.Id,
                EmpleadoId = beneficiarioId,
                MontoComision = monto,
                BaseCalculo = baseCalculo,
                TasaPorcentaje = rep?.TipoCalculo == "porcentaje" ? rep.Valor : (decimal?)null,
                MontoFijo = rep?.TipoCalculo == "fijo" ? rep.Valor : (decimal?)null,
                FechaCalculo = DateTime.UtcNow,
                Estado = esVentaGanada ? EstadoComisionValue.Activa : EstadoComisionValue.Inactiva,
                Tipo = (rep != null && rep.EquipoId.HasValue) ? TipoComisionValue.GestionEquipo : TipoComisionValue.VentaCerrada,
                ProveedorId = regla.ProveedorId,
                AppliedReglaNombre = regla.Nombre,
                AppliedTierNombre = tier?.Nombre ?? "Base",
                AppliedTierId = tier?.Id,
                Notas = $"{notas}. { (rep != null && rep.RolId.HasValue ? "Rol Match" : "") }",
                Periodo = await GetPeriodNameForDate(venta.FechaInstalacionReal ?? venta.FechaVenta)
            };
            _context.Comisiones.Add(com);
        }

        private string GetPeriodNameForDateSync(DateTime fecha)
        {
            // Helper síncrono para simplificar llamadas dentro de bucles si no queremos awaitar el nombre cada vez
            return fecha.ToString("MMMM yyyy");
        }

        private async Task<string> GetPeriodNameForDate(DateTime fecha)
        {
            var periodo = await _context.PeriodosFacturacion
                .FirstOrDefaultAsync(p => fecha.Date >= p.FechaInicio.Date && fecha.Date <= p.FechaFin.Date);
            return periodo?.Nombre ?? fecha.ToString("MMMM yyyy");
        }

        private async Task<decimal> CalculateVolumeForPeriod(int usuarioId, string variable, DateTime fecha)
        {
            var periodo = await _context.PeriodosFacturacion
                .FirstOrDefaultAsync(p => fecha.Date >= p.FechaInicio.Date && fecha.Date <= p.FechaFin.Date);

            if (periodo == null) return 0;

            var ventasPeriodo = await _context.Ventas
                .Where(v => v.UsuarioId == usuarioId && 
                            ((v.FechaInstalacionReal != null && v.FechaInstalacionReal.Value.Date >= periodo.FechaInicio.Date && v.FechaInstalacionReal.Value.Date <= periodo.FechaFin.Date)
                             || (v.FechaInstalacionReal == null && v.FechaVenta.Date >= periodo.FechaInicio.Date && v.FechaVenta.Date <= periodo.FechaFin.Date)) &&
                            v.Estado != null && v.Estado.EsGanada) 
                .ToListAsync();

            if (variable == "total_ventas") return ventasPeriodo.Count;
            if (variable == "total_monto" || variable == "monto_total") return ventasPeriodo.Sum(v => v.MontoTotal);

            return 0;
        }

        public async Task RecalculatePeriod(int periodoId)
        {
            var periodo = await _context.PeriodosFacturacion.FindAsync(periodoId);
            if (periodo == null) return;

            // Buscamos ventas cuya FECHA de anclaje (Instalación o Venta) caiga en el periodo
            var ventas = await _context.Ventas
                .Where(v => 
                    (v.FechaInstalacionReal != null && v.FechaInstalacionReal.Value.Date >= periodo.FechaInicio.Date && v.FechaInstalacionReal.Value.Date <= periodo.FechaFin.Date)
                    || 
                    (v.FechaInstalacionReal == null && v.FechaVenta.Date >= periodo.FechaInicio.Date && v.FechaVenta.Date <= periodo.FechaFin.Date)
                )
                .Select(v => v.Id)
                .ToListAsync();

            foreach (var vId in ventas)
            {
                await CalculateCommissionsForSale(vId, false);
            }
        }

        public decimal GetProjectedMargin(int reglaId)
        {
            // Lógica para el validador de margen (Front/Back)
            return 0; // Implementar después
        }
    }
}
