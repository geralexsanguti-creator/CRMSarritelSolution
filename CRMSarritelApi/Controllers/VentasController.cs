using CRMSarritelApi.Data;
using CRMSarritelApi.DTOs;
using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMSarritelApi.Services;
using System.Security.Claims;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentasController(
        IGenericRepository<Venta, int> repository, 
        CRMSarritelDbContext context,
        ICommissionService commissionService) : ControllerBase
    {
        private readonly IGenericRepository<Venta, int> _repository = repository;
        private readonly CRMSarritelDbContext _context = context;
        private readonly ICommissionService _commissionService = commissionService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                          ?? User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int currentUserId))
                return Unauthorized();

            bool canViewAll = User.IsInRole("Admin") || await HasPermission(currentUserId, "ventas:view_all");

            IQueryable<Venta> query = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Include(v => v.TipoVenta)
                .Include(v => v.Detalles).ThenInclude(d => d.Producto);

            if (!canViewAll)
            {
                // Obtener equipos donde el usuario es Manager
                var managedTeamIds = await _context.UsuarioEquipos
                    .Where(ue => ue.UsuarioId == currentUserId && ue.EsManager)
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

                    // Filtrar por ventas propias O de miembros del equipo
                    query = query.Where(v => v.UsuarioId == currentUserId || teamMemberIds.Contains(v.UsuarioId));
                }
                else
                {
                    // Solo ventas propias
                    query = query.Where(v => v.UsuarioId == currentUserId);
                }
            }

            var ventas = await query
                .OrderByDescending(v => v.FechaVenta)
                .Select(v => new VentaDto
                {
                    Id = v.Id,
                    NumeroVenta = v.NumeroVenta,
                    MontoTotal = v.MontoTotal,
                    FechaVenta = v.FechaVenta,
                    ClienteId = v.ClienteId,
                    UsuarioId = v.UsuarioId,
                    ClienteNombre = v.Cliente != null ? v.Cliente.Nombre : "Desconocido",
                    UsuarioNombre = v.Usuario != null ? v.Usuario.Nombre : "Desconocido",
                    Estado_Codigo = v.Estado.Codigo,
                    Estado_Nombre = v.Estado.Nombre,
                    Estado_Color = v.Estado.Color,
                    Estado_Icono = v.Estado.Icono,
                    TipoVenta_Codigo = v.TipoVenta != null ? v.TipoVenta.Codigo ?? "" : "",
                    TipoVenta_Nombre = v.TipoVenta != null ? v.TipoVenta.Nombre : "N/A",
                    TipoVenta_Descripcion = v.TipoVenta != null ? v.TipoVenta.Descripcion ?? "" : "",
                    OrigenVenta = v.OrigenVenta ?? "",
                    TipoVentaId = v.TipoVentaId,
                    Notas = v.Notas ?? "",
                    MontoVenta = v.MontoVenta,
                    DescuentoPorcentaje = v.DescuentoPorcentaje,
                    DescuentoMonto = v.DescuentoMonto,
                    FechaInstalacionPrevista = v.FechaInstalacionPrevista,
                    FechaInstalacionReal = v.FechaInstalacionReal,
                    Detalles = v.Detalles.Select(d => new DetalleVentaDto {
                        Id = d.Id,
                        IdProducto = d.IdProducto ?? 0,
                        ProductoNombre = d.Producto != null ? d.Producto.Nombre : "",
                        Cantidad = d.Cantidad,
                        Total = d.Total,
                        DatosConfiguracion = d.DatosConfiguracion
                    }).ToList()
                })
                .ToListAsync();

            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                          ?? User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int currentUserId))
                return Unauthorized();

            bool canViewAll = User.IsInRole("Admin") || await HasPermission(currentUserId, "ventas:view_all");

            var query = _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Include(v => v.TipoVenta)
                .Include(v => v.Detalles).ThenInclude(d => d.Producto)
                .AsQueryable();

            if (!canViewAll)
            {
                var managedTeamIds = await _context.UsuarioEquipos
                    .Where(ue => ue.UsuarioId == currentUserId && ue.EsManager)
                    .Select(ue => ue.EquipoId)
                    .ToListAsync();

                if (managedTeamIds.Any())
                {
                    var teamMemberIds = await _context.UsuarioEquipos
                        .Where(ue => managedTeamIds.Contains(ue.EquipoId))
                        .Select(ue => ue.UsuarioId)
                        .ToListAsync();

                    query = query.Where(v => v.Id == id && (v.UsuarioId == currentUserId || teamMemberIds.Contains(v.UsuarioId)));
                }
                else
                {
                    query = query.Where(v => v.Id == id && v.UsuarioId == currentUserId);
                }
            }
            else
            {
                query = query.Where(v => v.Id == id);
            }

            var venta = await query
                .Select(v => new VentaDto
                {
                    Id = v.Id,
                    NumeroVenta = v.NumeroVenta,
                    MontoTotal = v.MontoTotal,
                    FechaVenta = v.FechaVenta,
                    ClienteId = v.ClienteId,
                    UsuarioId = v.UsuarioId,
                    ClienteNombre = v.Cliente != null ? v.Cliente.Nombre : "Desconocido",
                    UsuarioNombre = v.Usuario != null ? v.Usuario.Nombre : "Desconocido",
                    Estado_Codigo = v.Estado.Codigo,
                    Estado_Nombre = v.Estado.Nombre,
                    Estado_Color = v.Estado.Color,
                    Estado_Icono = v.Estado.Icono,
                    TipoVenta_Codigo = v.TipoVenta != null ? v.TipoVenta.Codigo ?? "" : "",
                    TipoVenta_Nombre = v.TipoVenta != null ? v.TipoVenta.Nombre : "N/A",
                    TipoVenta_Descripcion = v.TipoVenta != null ? v.TipoVenta.Descripcion ?? "" : "",
                    OrigenVenta = v.OrigenVenta ?? "",
                    TipoVentaId = v.TipoVentaId,
                    Notas = v.Notas ?? "",
                    MontoVenta = v.MontoVenta,
                    DescuentoPorcentaje = v.DescuentoPorcentaje,
                    DescuentoMonto = v.DescuentoMonto,
                    FechaInstalacionPrevista = v.FechaInstalacionPrevista,
                    FechaInstalacionReal = v.FechaInstalacionReal,
                    Detalles = v.Detalles.Select(d => new DetalleVentaDto {
                        Id = d.Id,
                        IdProducto = d.IdProducto ?? 0,
                        ProductoNombre = d.Producto != null ? d.Producto.Nombre : "",
                        Cantidad = d.Cantidad,
                        Total = d.Total,
                        DatosConfiguracion = d.DatosConfiguracion
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (venta == null) return NotFound();
            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VentaDto dto)
        {
            var venta = new Venta
            {
                NumeroVenta = dto.NumeroVenta,
                MontoTotal = dto.MontoTotal,
                FechaVenta = DateTime.SpecifyKind(dto.FechaVenta.Date, DateTimeKind.Utc),
                FechaInstalacionPrevista = dto.FechaInstalacionPrevista.HasValue 
                    ? DateTime.SpecifyKind(dto.FechaInstalacionPrevista.Value.Date, DateTimeKind.Utc) 
                    : null,
                FechaInstalacionReal = dto.FechaInstalacionReal.HasValue 
                    ? DateTime.SpecifyKind(dto.FechaInstalacionReal.Value.Date, DateTimeKind.Utc) 
                    : null,
                ClienteId = dto.ClienteId,
                UsuarioId = dto.UsuarioId,
                OrigenVenta = dto.OrigenVenta,
                Notas = "", // Opcional
                Estado = new EstadoVentaValue
                {
                    Codigo = dto.Estado_Codigo,
                    Nombre = dto.Estado_Nombre,
                    Color = dto.Estado_Color,
                    Icono = dto.Estado_Icono,
                    EsInicial = false,
                    EsGanada = dto.Estado_EsGanada,
                    EsFinal = dto.Estado_EsFinal
                },
            };

            // Intentar buscar el ID del tipo de venta por código o nombre
            var tipoVenta = await _context.TiposVentas
                .FirstOrDefaultAsync(t => t.Codigo == dto.TipoVenta_Codigo || t.Nombre == dto.TipoVenta_Nombre);
            
            if (tipoVenta != null)
            {
                venta.TipoVentaId = tipoVenta.Id;

                if (!string.IsNullOrEmpty(tipoVenta.EstadosVentaJson))
                {
                    try
                    {
                        var defs = System.Text.Json.JsonSerializer.Deserialize<List<EstadoVentaValue>>(tipoVenta.EstadosVentaJson, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        var match = defs?.FirstOrDefault(d => string.Equals(d.Codigo, dto.Estado_Codigo, StringComparison.OrdinalIgnoreCase));
                        if (match != null && match.EsGanada) venta.Estado.EsGanada = true;
                        
                        var estadosArr = defs; // Reusing defs for the rest of parameters
                        // Búsqueda robusta por código o nombre (ignorando mayúsculas)
                        var estadoConf = estadosArr?.FirstOrDefault(e => 
                            string.Equals(e.Codigo, dto.Estado_Codigo, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(e.Nombre, dto.Estado_Nombre, StringComparison.OrdinalIgnoreCase)
                        );
                        
                        if (estadoConf != null && estadoConf.EsGanada)
                        {
                            venta.Estado.EsGanada = true;
                        }

                        if (estadoConf != null) 
                        {
                            venta.Estado.EsFinal = estadoConf.EsFinal;
                            venta.Estado.Color = estadoConf.Color;
                            venta.Estado.Icono = estadoConf.Icono;
                            venta.Estado.EsInicial = estadoConf.EsInicial;
                        }

                        // Heurística de respaldo: Si el nombre contiene GANADA o ACTIVO, forzar EsGanada
                        var lowerName = (dto.Estado_Nombre ?? "").ToLower();
                        if (!venta.Estado.EsGanada && (lowerName.Contains("ganada") || lowerName.Contains("activo") || lowerName.Contains("completada") || lowerName.Contains("won")))
                        {
                            venta.Estado.EsGanada = true;
                        }
                    } catch { } 
                }
            }

            if (dto.Detalles != null)
            {
                foreach (var d in dto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(d.IdProducto);
                    if (producto != null)
                    {
                        if (!producto.EsInfinito)
                        {
                            if (producto.Cantidad < d.Cantidad)
                            {
                                return BadRequest(new { message = $"Stock insuficiente para el producto {producto.Nombre}. Solicita {d.Cantidad} pero solo hay {producto.Cantidad}." });
                            }
                            producto.Cantidad -= d.Cantidad;
                            _context.Productos.Update(producto);
                        }
                        
                        venta.Detalles.Add(new DetalleVenta
                        {
                            IdProducto = d.IdProducto,
                            Cantidad = d.Cantidad,
                            Total = d.Total,
                            DatosConfiguracion = d.DatosConfiguracion,
                            Venta = venta, // Assigned for required property
                            Producto = null! // Handled by IdProducto
                        });
                    }
                }
            }

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            // Log Creation
            await LogChange(venta.Id, "Creación", "Venta registrada en el sistema", "Venta", null, venta.NumeroVenta);

            // --- FASE 3: Generar Comisiones Automáticamente ---
            try {
                await _commissionService.CalculateCommissionsForSale(venta.Id);
            } catch (Exception ex) {
                // Loguear error pero no bloquear la venta
                Console.WriteLine($"ERROR al calcular comisiones: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetById), new { id = venta.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VentaDto dto)
        {
            if (id != dto.Id) return BadRequest();
            
            var venta = await _context.Ventas.Include(v => v.Detalles).FirstOrDefaultAsync(v => v.Id == id);
            if (venta == null) return NotFound();

            // Log modifications before saving changes (to compare with current values in DB)
            if (venta.Estado.Codigo != dto.Estado_Codigo)
            {
                await LogChange(venta.Id, "Estado", $"Estado cambiado de {venta.Estado.Nombre} a {dto.Estado_Nombre}", "Estado", venta.Estado.Nombre, dto.Estado_Nombre);
            }
            if (Math.Abs(venta.MontoTotal - dto.MontoTotal) > 0.01m)
            {
                await LogChange(venta.Id, "Finanzas", $"Monto total cambiado de {venta.MontoTotal} a {dto.MontoTotal}", "MontoTotal", venta.MontoTotal.ToString(), dto.MontoTotal.ToString());
            }
            if (venta.Notas != dto.Notas)
            {
                await LogChange(venta.Id, "Notas", "Se han actualizado las notas internas de la venta", "Notas");
            }
            if (venta.FechaInstalacionReal != dto.FechaInstalacionReal)
            {
                await LogChange(venta.Id, "Instalación", $"Fecha de instalación real actualizada a {dto.FechaInstalacionReal?.ToShortDateString() ?? "N/A"}", "FechaInstalacionReal", venta.FechaInstalacionReal?.ToShortDateString(), dto.FechaInstalacionReal?.ToShortDateString());
            }

            // Update main fields
            venta.NumeroVenta = dto.NumeroVenta;
            venta.Estado.Codigo = dto.Estado_Codigo;
            venta.Estado.Nombre = dto.Estado_Nombre;
            venta.Estado.EsGanada = dto.Estado_EsGanada;
            venta.Estado.EsFinal = dto.Estado_EsFinal;
            venta.Estado.Icono = dto.Estado_Icono;
            venta.Estado.Color = dto.Estado_Color;
            
            // Financiero
            venta.MontoVenta = dto.MontoVenta;
            venta.DescuentoPorcentaje = dto.DescuentoPorcentaje;
            venta.DescuentoMonto = dto.DescuentoMonto;
            venta.MontoTotal = dto.MontoTotal;
            
            // Fechas
            venta.FechaVenta = DateTime.SpecifyKind(dto.FechaVenta.Date, DateTimeKind.Utc);
            venta.FechaInstalacionPrevista = dto.FechaInstalacionPrevista.HasValue 
                ? DateTime.SpecifyKind(dto.FechaInstalacionPrevista.Value.Date, DateTimeKind.Utc) 
                : null;
            venta.FechaInstalacionReal = dto.FechaInstalacionReal.HasValue 
                ? DateTime.SpecifyKind(dto.FechaInstalacionReal.Value.Date, DateTimeKind.Utc) 
                : null;
            venta.Notas = dto.Notas;
            if (!string.IsNullOrEmpty(dto.OrigenVenta)) venta.OrigenVenta = dto.OrigenVenta;
            
            venta.ClienteId = dto.ClienteId;
            venta.UsuarioId = dto.UsuarioId;

            // Procesar Detalles
            if (dto.Detalles != null)
            {
                foreach (var d in dto.Detalles)
                {
                    if (d.Id == 0) // Nuevo
                    {
                        var producto = await _context.Productos.FindAsync(d.IdProducto);
                        if (producto != null)
                        {
                            if (!producto.EsInfinito)
                            {
                                if (producto.Cantidad < d.Cantidad) return BadRequest(new { message = "Stock insuficiente" });
                                producto.Cantidad -= d.Cantidad;
                                _context.Productos.Update(producto);
                            }
                            venta.Detalles.Add(new DetalleVenta { IdProducto = d.IdProducto, Cantidad = d.Cantidad, Total = d.Total, DatosConfiguracion = d.DatosConfiguracion, Venta = venta, Producto = null! });
                        }
                    }
                    else // Existente
                    {
                        var existingDetail = venta.Detalles.FirstOrDefault(det => det.Id == d.Id);
                        if (existingDetail != null)
                        {
                            // Actualizar configuración si ha cambiado
                            if (existingDetail.DatosConfiguracion != d.DatosConfiguracion)
                            {
                                existingDetail.DatosConfiguracion = d.DatosConfiguracion;
                                await LogChange(venta.Id, "Configuración", "Se han actualizado los datos de configuración del servicio", "DatosConfiguracion");
                            }
                            existingDetail.Cantidad = d.Cantidad;
                            existingDetail.Total = d.Total;
                        }
                    }
                }
                var inputIds = dto.Detalles.Select(d => d.Id).ToList();
                var aEliminar = venta.Detalles.Where(d => d.Id != 0 && !inputIds.Contains(d.Id)).ToList();
                foreach (var det in aEliminar)
                {
                    var prod = await _context.Productos.FindAsync(det.IdProducto);
                    if (prod != null && !prod.EsInfinito) { prod.Cantidad += det.Cantidad; _context.Productos.Update(prod); }
                    _context.DetalleVentas.Remove(det);
                    venta.Detalles.Remove(det);
                }
            }

            await _context.SaveChangesAsync();

            try { await _commissionService.CalculateCommissionsForSale(venta.Id); } catch { }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return NotFound();
            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/historial")]
        public async Task<IActionResult> GetHistorial(int id)
        {
            var history = await _context.HistorialVentas
                .Include(h => h.Usuario)
                .Where(h => h.VentaId == id)
                .OrderByDescending(h => h.FechaCambio)
                .Select(h => new {
                    h.Id,
                    h.FechaCambio,
                    h.Accion,
                    h.Descripcion,
                    UsuarioNombre = h.Usuario != null ? h.Usuario.Nombre : "Sistema",
                    h.CampoModificado,
                    h.ValorAnterior,
                    h.ValorNuevo
                })
                .ToListAsync();
            return Ok(history);
        }

        private async Task<bool> HasPermission(int userId, string permissionName)
        {
            return await _context.UsuarioRoles
                .Where(ur => ur.UsuarioId == userId)
                .SelectMany(ur => ur.Rol.RolPermisos)
                .AnyAsync(rp => rp.Permiso.Nombre == permissionName);
        }

        private async Task LogChange(int ventaId, string accion, string descripcion, string? campo = null, string? anterior = null, string? nuevo = null)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                          ?? User.FindFirst("sub")?.Value 
                          ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
                          
            int? userId = int.TryParse(userIdStr, out int id) ? id : null;

            if (userId == null)
            {
                Console.WriteLine($"[AUDIT] Advertencia: No se pudo identificar al usuario para la acción '{accion}' en la venta {ventaId}. Claims: {string.Join(", ", User.Claims.Select(c => $"{c.Type}={c.Value}"))}");
            }

            var log = new HistorialVenta
            {
                VentaId = ventaId,
                UsuarioId = userId,
                FechaCambio = DateTime.UtcNow,
                Accion = accion,
                Descripcion = descripcion,
                CampoModificado = campo,
                ValorAnterior = anterior,
                ValorNuevo = nuevo
            };

            _context.HistorialVentas.Add(log);
        }
    }
}
