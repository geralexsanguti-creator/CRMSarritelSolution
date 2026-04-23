using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMSarritelApi.Services;
using CRMSarritelApi.DTOs;
using CRMSarritelApi.Data;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComisionesController(
        IGenericRepository<Comision, int> repository,
        ICommissionService commissionService,
        CRMSarritelDbContext context) : ControllerBase
    {
        private readonly IGenericRepository<Comision, int> _repository = repository;
        private readonly ICommissionService _commissionService = commissionService;
        private readonly CRMSarritelDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = HttpContext.User;
            var userIdStr = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = user.IsInRole("Admin") || user.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.Role && c.Value == "Admin");
            
            var canViewAll = isAdmin || await HasPermission(int.TryParse(userIdStr, out int id) ? id : 0, "comisiones:view_all");

            var query = _repository.GetAll()
                .Include(c => c.Empleado)
                .Include(c => c.Venta!).ThenInclude(v => v.Usuario)
                .Include(c => c.DetalleVenta).ThenInclude(dv => dv!.Producto).ThenInclude(p => p!.Proveedor)
                .Include(c => c.Proveedor)
                .AsQueryable();

            if (!canViewAll && int.TryParse(userIdStr, out int userId))
            {
                // Obtener equipos donde el usuario es Manager
                var managedTeamIds = await _context.UsuarioEquipos
                    .Where(ue => ue.UsuarioId == userId && ue.EsManager)
                    .Select(ue => ue.EquipoId)
                    .ToListAsync();

                if (managedTeamIds.Any())
                {
                    // Obtener IDs de todos los miembros de esos equipos
                    var teamMemberIds = await _context.UsuarioEquipos
                        .Where(ue => managedTeamIds.Contains(ue.EquipoId))
                        .Select(ue => ue.UsuarioId)
                        .Distinct()
                        .ToListAsync();

                    // Filtrar por comisiones propias O de miembros del equipo
                    query = query.Where(c => c.EmpleadoId == userId || teamMemberIds.Contains(c.EmpleadoId));
                }
                else
                {
                    // Solo comisiones propias
                    query = query.Where(c => c.EmpleadoId == userId);
                }
            }

            var items = await query.ToListAsync();
            var dtos = items.Select(c => new ComisionDto
            {
                Id = c.Id,
                VentaId = c.VentaId,
                Venta_Numero = c.Venta?.NumeroVenta ?? "-",
                DetalleVentaId = c.DetalleVentaId,
                ProductoNombre = c.DetalleVenta?.Producto?.Nombre ?? (c.ProveedorId != null || c.DetalleVenta?.Producto?.ProveedorId != null ? $"Concepto Extra ({(c.Proveedor?.Nombre ?? c.DetalleVenta?.Producto?.Proveedor?.Nombre)})" : "Venta Manual"),
                ProductoIcono = c.DetalleVenta?.Producto?.Imagen ?? (c.ProveedorId != null || c.DetalleVenta?.Producto?.ProveedorId != null ? "🌟" : "📦"),
                ProveedorId = c.ProveedorId ?? c.DetalleVenta?.Producto?.ProveedorId,
                ProveedorNombre = c.Proveedor?.Nombre ?? c.DetalleVenta?.Producto?.Proveedor?.Nombre,
                EmpleadoId = c.EmpleadoId,
                EmpleadoNombre = c.Empleado?.Nombre ?? "Desconocido",
                Periodo = c.Periodo,
                Tipo_Nombre = c.Tipo.Nombre,
                MontoComision = c.MontoComision,
                BaseCalculo = c.BaseCalculo,
                TasaPorcentaje = c.TasaPorcentaje,
                MontoFijo = c.MontoFijo,
                Estado_Codigo = c.Estado.Codigo,
                Estado_Nombre = c.Estado.Nombre,
                Estado_Color = c.Estado.Color,
                Estado_Icono = c.Estado.Icono,
                Estado_EsPagable = c.Estado.EsPagable,
                FechaCalculo = c.FechaCalculo,
                FechaPago = c.FechaPago,
                CreatedAt = c.CreatedAt,
                VendedorId = c.Venta?.UsuarioId,
                VendedorNombre = c.Venta?.Usuario?.Nombre ?? "-",
                Notas = c.Notas,
                EsExtra = c.EsExtra
            }).ToList();
            
            return Ok(dtos);
        }

        [HttpGet("sale-summary/{ventaId}")]
        public async Task<IActionResult> GetSaleSummary(int ventaId, [FromQuery] int? detalleVentaId = null)
        {
            var query = _repository.GetAll()
                .Include(c => c.Empleado)
                .Include(c => c.Venta)
                .Include(c => c.DetalleVenta).ThenInclude(dv => dv!.Producto)
                .Where(c => c.VentaId == ventaId);

            if (detalleVentaId.HasValue && detalleVentaId > 0)
            {
                query = query.Where(c => c.DetalleVentaId == detalleVentaId);
            }

            var comisiones = await query.ToListAsync();

            if (!comisiones.Any()) 
            {
                // Si no hay comisiones, intentamos sacar la base del detalle directo si existe
                decimal baseDirecta = 0;
                string numeroVenta = "N/A";
                
                if (detalleVentaId.HasValue && detalleVentaId > 0)
                {
                    var dv = await _context.DetalleVentas.Include(d => d.Venta).FirstOrDefaultAsync(d => d.Id == detalleVentaId);
                    if (dv != null)
                    {
                        baseDirecta = dv.Total; // O la base que corresponda
                        numeroVenta = dv.Venta?.NumeroVenta ?? "N/A";
                    }
                }

                return Ok(new SaleCommissionSummaryDto
                {
                    VentaId = ventaId,
                    DetalleVentaId = detalleVentaId,
                    NumeroVenta = numeroVenta,
                    BaseBruta = baseDirecta,
                    TotalComisionado = 0,
                    Remanente = baseDirecta,
                    Beneficiarios = new List<BeneficiarySummaryDto>()
                });
            }

            var first = comisiones.FirstOrDefault();
            var summary = new SaleCommissionSummaryDto
            {
                VentaId = ventaId,
                DetalleVentaId = detalleVentaId,
                NumeroVenta = first?.Venta?.NumeroVenta ?? "N/A",
                BaseBruta = first?.BaseCalculo ?? 0,
                TotalComisionado = comisiones.Where(c => c.EmpleadoId != 99).Sum(c => c.MontoComision),
                Remanente = comisiones.FirstOrDefault(c => c.EmpleadoId == 99)?.MontoComision ?? (first?.BaseCalculo ?? 0) - comisiones.Where(c => c.EmpleadoId != 99).Sum(c => c.MontoComision),
                Beneficiarios = comisiones.Select(c => new BeneficiarySummaryDto
                {
                    Id = c.Id, // Añadimos ID para filtrar en el front
                    EmpleadoId = c.EmpleadoId,
                    Nombre = c.Empleado?.Nombre ?? (c.EmpleadoId == 99 ? "SISTEMA" : "Desconocido"),
                    Monto = c.MontoComision,
                    Tipo = c.Tipo.Nombre,
                    Estado = c.Estado.Nombre
                }).ToList()
            };

            return Ok(summary);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _repository.GetAll()
                .Include(c => c.Empleado)
                .Include(c => c.Venta!).ThenInclude(v => v.Usuario)
                .Include(c => c.DetalleVenta).ThenInclude(dv => dv!.Producto).ThenInclude(p => p!.Proveedor)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(c => c.Id == id);
                
            if (c == null) return NotFound();

            var dto = new ComisionDto
            {
                Id = c.Id,
                VentaId = c.VentaId,
                Venta_Numero = c.Venta?.NumeroVenta ?? "-",
                DetalleVentaId = c.DetalleVentaId,
                ProductoNombre = c.DetalleVenta?.Producto?.Nombre ?? (c.ProveedorId != null ? $"Concepto Extra ({c.Proveedor?.Nombre})" : "Venta Manual"),
                ProductoIcono = c.DetalleVenta?.Producto?.Imagen ?? (c.ProveedorId != null ? "🌟" : "📦"),
                ProveedorId = c.ProveedorId,
                ProveedorNombre = c.Proveedor?.Nombre,
                EmpleadoId = c.EmpleadoId,
                EmpleadoNombre = c.Empleado?.Nombre ?? "Desconocido",
                Periodo = c.Periodo,
                Tipo_Nombre = c.Tipo.Nombre,
                MontoComision = c.MontoComision,
                BaseCalculo = c.BaseCalculo,
                TasaPorcentaje = c.TasaPorcentaje,
                MontoFijo = c.MontoFijo,
                Estado_Codigo = c.Estado.Codigo,
                Estado_Nombre = c.Estado.Nombre,
                Estado_Color = c.Estado.Color,
                Estado_Icono = c.Estado.Icono,
                Estado_EsPagable = c.Estado.EsPagable,
                FechaCalculo = c.FechaCalculo,
                FechaPago = c.FechaPago,
                CreatedAt = c.CreatedAt,
                VendedorId = c.Venta?.UsuarioId,
                VendedorNombre = c.Venta?.Usuario?.Nombre ?? "-",
                Notas = c.Notas,
                EsExtra = c.EsExtra
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comision comision)
        {
            comision.CreatedAt = DateTime.UtcNow;
            
            // Refinamiento del mapeo manual
            if (comision.Tipo == null || string.IsNullOrEmpty(comision.Tipo.Codigo))
            {
                comision.Tipo = TipoComisionValue.Manual;
                comision.Tipo.Codigo = "MANUAL";
            }

            if (comision.Estado == null || string.IsNullOrEmpty(comision.Estado.Codigo))
            {
                comision.Estado = EstadoComisionValue.Activa;
            }

            // --- BLOQUE DE SEGURIDAD: CIERRE DE PRESUPUESTO ---
            if (comision.DetalleVentaId.HasValue)
            {
                var sumExistente = await _repository.GetAll()
                    .Where(c => c.DetalleVentaId == comision.DetalleVentaId)
                    .SumAsync(c => (decimal?)c.MontoComision) ?? 0;

                if (sumExistente + comision.MontoComision > (comision.BaseCalculo + 0.01m)) // Margen de error decimal minúsculo
                {
                    return BadRequest(new { 
                        error = "DÉFICIT PRESUPUESTARIO", 
                        message = $"La suma de comisiones ({sumExistente + comision.MontoComision}€) superaría el total del producto ({comision.BaseCalculo}€)." 
                    });
                }
            }

            await _repository.Insertar(comision);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = comision.Id }, comision);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Comision comision)
        {
            if (id != comision.Id) return BadRequest();

            // --- BLOQUE DE SEGURIDAD: CIERRE DE PRESUPUESTO ---
            if (comision.DetalleVentaId.HasValue)
            {
                var sumExistente = await _repository.GetAll()
                    .Where(c => c.DetalleVentaId == comision.DetalleVentaId && c.Id != id)
                    .SumAsync(c => (decimal?)c.MontoComision) ?? 0;

                if (sumExistente + comision.MontoComision > (comision.BaseCalculo + 0.01m))
                {
                    return BadRequest(new { 
                        error = "DÉFICIT PRESUPUESTARIO", 
                        message = $"La edición superaría el total del producto ({comision.BaseCalculo}€). Actual: {sumExistente}€ + Nueva: {comision.MontoComision}€" 
                    });
                }
            }

            // Lógica de Split: si cambian el Empleado y le asignan una parte menor del total...
            var oldItem = await _repository.GetAll().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            
            if (oldItem != null && oldItem.EmpleadoId != comision.EmpleadoId)
            {
                decimal remainder = oldItem.MontoComision - comision.MontoComision;
                
                if (remainder > 0)
                {
                    // 1. El original se queda la parte que sobra (el 'remanente')
                    oldItem.MontoComision = remainder;
                    oldItem.MontoFijo = null;
                    oldItem.TasaPorcentaje = null; 
                    _repository.Actualizar(oldItem);

                    // 2. Crear una nueva comisión con la porción extraída para el nuevo dueño
                    comision.Id = 0; // Forzar inserción
                    comision.Tipo = TipoComisionValue.Extraida;
                    comision.Tipo.Codigo = "MANUAL"; // Marcamos como manual para que se vea la M
                    comision.Tipo.Nombre = (comision.Tipo.Nombre ?? "Comisión") + " (Extraída)";
                    await _repository.Insertar(comision);
                    
                    await _repository.SaveChangesAsync();
                    return NoContent();
                }
            }

            _repository.Actualizar(comision);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comision = await _context.Comisiones.FindAsync(id);
            if (comision == null) return NotFound();

            // Si es la del sistema (99), simplemente la borramos (aunque no se recomienda)
            if (comision.EmpleadoId == 99)
            {
                _context.Comisiones.Remove(comision);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            // Si es una comisión humana, el dinero debe volver a la "Empresa" (SISTEMA - ID 99)
            var sistemaComm = await _context.Comisiones
                .FirstOrDefaultAsync(c => c.VentaId == comision.VentaId && 
                                         c.DetalleVentaId == comision.DetalleVentaId && 
                                         c.EmpleadoId == 99);

            if (sistemaComm != null)
            {
                sistemaComm.MontoComision += comision.MontoComision;
                sistemaComm.UpdatedAt = DateTime.UtcNow;
                // Marcamos como manual para que el recálculo estándar (sin force) no la pise
                if (sistemaComm.Tipo == null) sistemaComm.Tipo = new TipoComisionValue("MANUAL", "Margen Empresa (Ajustado)");
                else sistemaComm.Tipo.Codigo = "MANUAL";
                
                _context.Comisiones.Update(sistemaComm);
            }

            _context.Comisiones.Remove(comision);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("recalculate-period/{periodoId}")]
        public async Task<IActionResult> RecalculatePeriod(int periodoId)
        {
            await _commissionService.RecalculatePeriod(periodoId);
            return Ok(new { message = "Recálculo del periodo completado con éxito" });
        }

        [HttpPost("update-group")]
        public async Task<IActionResult> UpdateGroup([FromBody] SaleCommissionSummaryDto summary)
        {
            if (summary == null)
                return BadRequest("Datos insuficientes para la actualización.");

            try
            {
                // 1. Identificar el DetalleVentaId (Producto)
                int? detalleVentaId = summary.DetalleVentaId;
                if (detalleVentaId == null || detalleVentaId <= 0)
                {
                    detalleVentaId = summary.Beneficiarios.FirstOrDefault(b => b.DetalleVentaId > 0)?.DetalleVentaId;
                }

                if (detalleVentaId == null || detalleVentaId <= 0)
                {
                    return BadRequest(new { error = "ERROR_SINCRONIZACION", message = "No se pudo identificar el producto para la repartición." });
                }

                // 2. Obtener comisiones actuales del grupo (Venta + Producto)
                var currentCommissions = await _context.Comisiones
                    .Include(c => c.Tipo)
                    .Include(c => c.Estado)
                    .Where(c => c.VentaId == summary.VentaId && c.DetalleVentaId == detalleVentaId)
                    .ToListAsync();

                // 3. Identificar beneficiarios humanos entrantes
                var humanBeneficiaries = summary.Beneficiarios.Where(b => b.EmpleadoId != 99).ToList();
                var humanTotal = humanBeneficiaries.Sum(b => b.Monto);

                // 4. Validar presupuesto
                if (humanTotal > summary.BaseBruta + 0.10m)
                {
                    return BadRequest(new { error = "DÉFICIT PRESUPUESTARIO", message = $"La suma ({humanTotal}€) supera el bruto ({summary.BaseBruta}€)." });
                }

                // 5. Sincronizar: Altas y Modificaciones
                var processedIds = new HashSet<int>();
                var period = currentCommissions.FirstOrDefault(c => !string.IsNullOrEmpty(c.Periodo))?.Periodo 
                            ?? DateTime.Now.ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));

                foreach (var b in humanBeneficiaries)
                {
                    Comision? comm = null;

                    if (b.Id > 0)
                    {
                        comm = currentCommissions.FirstOrDefault(c => c.Id == b.Id);
                    }
                    
                    if (comm == null)
                    {
                        comm = currentCommissions.FirstOrDefault(c => c.EmpleadoId == b.EmpleadoId && !processedIds.Contains(c.Id));
                    }

                    if (comm != null)
                    {
                        comm.MontoComision = b.Monto;
                        comm.UpdatedAt = DateTime.UtcNow;
                        
                        if (comm.Tipo == null)
                        {
                            comm.Tipo = new TipoComisionValue("MANUAL", "Ajuste Manual");
                        }
                        else if (comm.Tipo.Codigo != "GESTION_EQUIPO")
                        {
                            comm.Tipo.Codigo = "MANUAL";
                            comm.Tipo.Nombre = "Ajuste Manual";
                        }
                        
                        if (comm.Estado == null)
                        {
                            comm.Estado = EstadoComisionValue.Activa;
                        }
                        
                        processedIds.Add(comm.Id);
                    }
                    else
                    {
                        var newComm = new Comision
                        {
                            VentaId = summary.VentaId,
                            DetalleVentaId = detalleVentaId,
                            EmpleadoId = b.EmpleadoId,
                            MontoComision = b.Monto,
                            BaseCalculo = summary.BaseBruta,
                            Periodo = period,
                            Tipo = new TipoComisionValue("MANUAL", "Ajuste Manual"),
                            Estado = EstadoComisionValue.Activa,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            Notas = "Añadido manualmente desde Repartición Real."
                        };
                        await _context.Comisiones.AddAsync(newComm);
                    }
                }

                foreach (var old in currentCommissions.ToList())
                {
                    if (old.EmpleadoId != 99 && !processedIds.Contains(old.Id))
                    {
                        _context.Comisiones.Remove(old);
                    }
                }

                var sistemaComm = currentCommissions.FirstOrDefault(c => c.EmpleadoId == 99);
                decimal sistemaMonto = summary.BaseBruta - humanTotal;

                if (sistemaComm != null)
                {
                    sistemaComm.MontoComision = Math.Max(0, sistemaMonto);
                    sistemaComm.UpdatedAt = DateTime.UtcNow;
                    if (sistemaComm.Tipo == null) sistemaComm.Tipo = new TipoComisionValue("MANUAL", "Margen Empresa (Ajustado)");
                    else sistemaComm.Tipo.Codigo = "MANUAL";
                }
                else
                {
                    var newSistema = new Comision
                    {
                        VentaId = summary.VentaId,
                        DetalleVentaId = detalleVentaId,
                        EmpleadoId = 99,
                        MontoComision = Math.Max(0, sistemaMonto),
                        BaseCalculo = summary.BaseBruta,
                        Periodo = period,
                        Tipo = new TipoComisionValue("MANUAL", "Margen Empresa (Ajustado)"),
                        Estado = EstadoComisionValue.Activa,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await _context.Comisiones.AddAsync(newSistema);
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Repartición sincronizada correctamente" });
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("update_group_error.log", ex.ToString());
                return StatusCode(500, $"Error al sincronizar el grupo: {ex.Message}");
            }
        }

        [HttpPost("recalculate-all")]
        [AllowAnonymous]
        public async Task<IActionResult> RecalculateAll([FromQuery] bool force = false)
        {
            var ventaIds = await _context.Ventas.Select(v => v.Id).ToListAsync();
            
            int count = 0;
            foreach (var vId in ventaIds)
            {
                await _commissionService.CalculateCommissionsForSale(vId, force);
                count++;
            }
            
            return Ok(new { message = $"Se han recalculado comisiones para {count} ventas.", totalVentas = count, forced = force });
        }

        private async Task<bool> HasPermission(int userId, string permissionName)
        {
            return await _context.UsuarioRoles
                .Where(ur => ur.UsuarioId == userId)
                .SelectMany(ur => ur.Rol.RolPermisos)
                .AnyAsync(rp => rp.Permiso.Nombre == permissionName);
        }
    }
}
