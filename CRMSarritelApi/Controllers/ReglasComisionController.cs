using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using CRMSarritelApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Controllers
{
    // ─── Input DTOs ───────────────────────────────────────────────────────────
    public class ReparticionComisionInputDto
    {
        public int? RolId { get; set; }
        public int? EquipoId { get; set; }
        public string TipoCalculo { get; set; } = "porcentaje";
        public decimal Valor { get; set; }
    }

    public class ReglaComisionTierInputDto
    {
        public string Nombre { get; set; } = "Nuevo Tier";
        public decimal ValorMin { get; set; }
        public decimal? ValorMax { get; set; }
        public string? TipoRemuneracionGross { get; set; }
        public decimal? ValorRemuneracionGross { get; set; }
        public List<ReparticionComisionInputDto> Reparticiones { get; set; } = new();
    }

    public class ReglaComisionInputDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Variable { get; set; } = "total_ventas";
        public string Operador { get; set; } = "=";
        public decimal? ValorMin { get; set; }
        public decimal? ValorMax { get; set; }
        public string TipoRemuneracionGross { get; set; } = "fijo";
        public decimal ValorRemuneracionGross { get; set; }
        public decimal ValorVenta { get; set; }
        public bool Activa { get; set; } = true;
        public int Prioridad { get; set; } = 0;
        public int ProveedorId { get; set; }
        public int? TipoVentaId { get; set; }
        public List<ReparticionComisionInputDto> ReparticionesBase { get; set; } = new();
        public List<ReglaComisionTierInputDto> Tiers { get; set; } = new();
    }

    // ─── Controller ──────────────────────────────────────────────────────────
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReglasComisionController(
        IGenericRepository<ReglaComision, int> repository,
        CRMSarritelDbContext dbContext) : ControllerBase
    {
        private readonly IGenericRepository<ReglaComision, int> _repository = repository;
        private readonly CRMSarritelDbContext _context = dbContext;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAll()
                .Include(r => r.TipoVenta)
                .AsNoTracking()
                .OrderByDescending(r => r.Prioridad)
                .ToListAsync();
            return Ok(items);
        }

        [HttpGet("proveedor/{proveedorId}")]
        public async Task<IActionResult> GetByProveedor(int proveedorId)
        {
            var items = await _repository.GetAll()
                .Include(r => r.TipoVenta)
                .Include(r => r.ReparticionesBase).ThenInclude(rb => rb.Rol)
                .Include(r => r.ReparticionesBase).ThenInclude(rb => rb.Equipo)
                .Include(r => r.Tiers)
                    .ThenInclude(t => t.Reparticiones).ThenInclude(tr => tr.Rol)
                .Include(r => r.Tiers)
                    .ThenInclude(t => t.Reparticiones).ThenInclude(tr => tr.Equipo)
                .Include(r => r.CarpetaReglasComision)
                    .ThenInclude(crc => crc.CarpetaReglas)
                .AsNoTracking()
                .AsSplitQuery()
                .Where(r => r.ProveedorId == proveedorId)
                .OrderByDescending(r => r.Prioridad)
                .ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetAll()
                .Include(r => r.TipoVenta)
                .Include(r => r.ReparticionesBase).ThenInclude(rb => rb.Rol)
                .Include(r => r.ReparticionesBase).ThenInclude(rb => rb.Equipo)
                .Include(r => r.Tiers)
                    .ThenInclude(t => t.Reparticiones).ThenInclude(tr => tr.Rol)
                .Include(r => r.Tiers)
                    .ThenInclude(t => t.Reparticiones).ThenInclude(tr => tr.Equipo)
                .Include(r => r.CarpetaReglasComision)
                    .ThenInclude(crc => crc.CarpetaReglas)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReglaComisionInputDto dto)
        {
            var regla = MapDtoToEntity(dto, 0);
            await _repository.Insertar(regla);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = regla.Id }, regla);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReglaComisionInputDto dto)
        {
            var existingRegla = await _context.ReglasComisiones
                .Include(r => r.ReparticionesBase)
                .Include(r => r.Tiers)
                    .ThenInclude(t => t.Reparticiones)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (existingRegla == null) return NotFound();

            // 1. Update scalar properties
            existingRegla.Nombre = dto.Nombre;
            existingRegla.Descripcion = dto.Descripcion;
            existingRegla.Variable = dto.Variable;
            existingRegla.Operador = dto.Operador;
            existingRegla.ValorMin = dto.ValorMin;
            existingRegla.ValorMax = dto.ValorMax;
            existingRegla.TipoRemuneracionGross = dto.TipoRemuneracionGross;
            existingRegla.ValorRemuneracionGross = dto.ValorRemuneracionGross;
            existingRegla.ValorVenta = dto.ValorVenta;
            existingRegla.Activa = dto.Activa;
            existingRegla.Prioridad = dto.Prioridad;
            existingRegla.ProveedorId = dto.ProveedorId;
            existingRegla.TipoVentaId = dto.TipoVentaId;

            // 2. Remove old collections
            _context.ReparticionesComision.RemoveRange(existingRegla.ReparticionesBase);
            foreach (var tier in existingRegla.Tiers)
                _context.ReparticionesComision.RemoveRange(tier.Reparticiones);
            _context.ReglaComisionTiers.RemoveRange(existingRegla.Tiers);

            // 3. Add new collections
            existingRegla.ReparticionesBase = dto.ReparticionesBase.Select(r => new ReparticionComision
            {
                RolId = r.RolId,
                EquipoId = r.EquipoId,
                TipoCalculo = r.TipoCalculo,
                Valor = r.Valor
            }).ToList();

            existingRegla.Tiers = dto.Tiers.Select(t => new ReglaComisionTier
            {
                Nombre = t.Nombre,
                ValorMin = t.ValorMin,
                ValorMax = t.ValorMax,
                TipoRemuneracionGross = t.TipoRemuneracionGross,
                ValorRemuneracionGross = t.ValorRemuneracionGross,
                Reparticiones = t.Reparticiones.Select(r => new ReparticionComision
                {
                    RolId = r.RolId,
                    EquipoId = r.EquipoId,
                    TipoCalculo = r.TipoCalculo,
                    Valor = r.Valor
                }).ToList()
            }).ToList();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/has-commissions")]
        public async Task<IActionResult> HasCommissions(int id)
        {
            bool hasHistory = await _context.Comisiones
                .AnyAsync(c => c.Venta != null && c.Venta.Detalles.Any(d =>
                    d.Producto != null && d.Producto.ProductoCarpetas.Any(pc =>
                        pc.CarpetaReglas != null &&
                        pc.CarpetaReglas.CarpetaReglasComision.Any(crc => crc.ReglaComisionId == id))));

            return Ok(new { hasCommissions = hasHistory });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DuroBorrado(id);
            if (!success) return NotFound();
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // ─── Helper ──────────────────────────────────────────────────────────
        private static ReglaComision MapDtoToEntity(ReglaComisionInputDto dto, int forceId)
        {
            return new ReglaComision
            {
                Id = forceId,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Variable = dto.Variable,
                Operador = dto.Operador,
                ValorMin = dto.ValorMin,
                ValorMax = dto.ValorMax,
                TipoRemuneracionGross = dto.TipoRemuneracionGross,
                ValorRemuneracionGross = dto.ValorRemuneracionGross,
                ValorVenta = dto.ValorVenta,
                Activa = dto.Activa,
                Prioridad = dto.Prioridad,
                ProveedorId = dto.ProveedorId,
                TipoVentaId = dto.TipoVentaId,
                ReparticionesBase = dto.ReparticionesBase.Select(r => new ReparticionComision
                {
                    RolId = r.RolId,
                    EquipoId = r.EquipoId,
                    TipoCalculo = r.TipoCalculo,
                    Valor = r.Valor
                }).ToList(),
                Tiers = dto.Tiers.Select(t => new ReglaComisionTier
                {
                    Nombre = t.Nombre,
                    ValorMin = t.ValorMin,
                    ValorMax = t.ValorMax,
                    TipoRemuneracionGross = t.TipoRemuneracionGross,
                    ValorRemuneracionGross = t.ValorRemuneracionGross,
                    Reparticiones = t.Reparticiones.Select(r => new ReparticionComision
                    {
                        RolId = r.RolId,
                        EquipoId = r.EquipoId,
                        TipoCalculo = r.TipoCalculo,
                        Valor = r.Valor
                    }).ToList()
                }).ToList()
            };
        }
    }
}
