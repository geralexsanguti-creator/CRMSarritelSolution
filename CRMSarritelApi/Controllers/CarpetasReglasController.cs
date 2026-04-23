using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Controllers
{
    // ─── Input DTOs ───────────────────────────────────────────────────────────
    public class CarpetaReglaComisionInputDto
    {
        public int ReglaComisionId { get; set; }
    }

    public class CarpetaReglasInputDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public int? ProveedorId { get; set; }
        public List<CarpetaReglaComisionInputDto> CarpetaReglasComision { get; set; } = new();
    }

    // ─── Controller ──────────────────────────────────────────────────────────
    [Route("api/[controller]")]
    [ApiController]
    public class CarpetasReglasController(CRMSarritelDbContext context) : ControllerBase
    {
        private readonly CRMSarritelDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carpetas = await _context.CarpetasReglas
                .Include(c => c.Proveedor)
                .Include(c => c.CarpetaReglasComision)
                    .ThenInclude(crc => crc.ReglaComision)
                .ToListAsync();
            return Ok(carpetas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var carpeta = await _context.CarpetasReglas
                .Include(c => c.CarpetaReglasComision)
                    .ThenInclude(crc => crc.ReglaComision)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carpeta == null) return NotFound();
            return Ok(carpeta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarpetaReglasInputDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { error = "El nombre es obligatorio" });

            var newCarpeta = new CarpetaReglas
            {
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                ProveedorId = dto.ProveedorId,
                CarpetaReglasComision = new List<CarpetaReglaComision>()
            };

            foreach (var crc in dto.CarpetaReglasComision)
            {
                newCarpeta.CarpetaReglasComision.Add(new CarpetaReglaComision
                {
                    ReglaComisionId = crc.ReglaComisionId
                });
            }

            _context.CarpetasReglas.Add(newCarpeta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = newCarpeta.Id }, newCarpeta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarpetaReglasInputDto dto)
        {
            var dbCarpeta = await _context.CarpetasReglas
                .Include(c => c.CarpetaReglasComision)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbCarpeta == null) return NotFound();

            dbCarpeta.Nombre = dto.Nombre;
            dbCarpeta.Activo = dto.Activo;
            dbCarpeta.ProveedorId = dto.ProveedorId;

            // Sync N:M rules
            var requestedReglaIds = dto.CarpetaReglasComision
                .Select(r => r.ReglaComisionId)
                .ToList();

            // Remove deselected
            var toRemove = dbCarpeta.CarpetaReglasComision
                .Where(crc => !requestedReglaIds.Contains(crc.ReglaComisionId))
                .ToList();
            foreach (var rm in toRemove)
                dbCarpeta.CarpetaReglasComision.Remove(rm);

            // Add new ones
            foreach (var rid in requestedReglaIds)
            {
                if (!dbCarpeta.CarpetaReglasComision.Any(c => c.ReglaComisionId == rid))
                {
                    dbCarpeta.CarpetaReglasComision.Add(new CarpetaReglaComision
                    {
                        CarpetaReglasId = id,
                        ReglaComisionId = rid
                    });
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dbCarpeta = await _context.CarpetasReglas.FindAsync(id);
            if (dbCarpeta == null) return NotFound();

            _context.CarpetasReglas.Remove(dbCarpeta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
