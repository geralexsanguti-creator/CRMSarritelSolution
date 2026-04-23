using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly CRMSarritelDbContext _context;

        public EquiposController(CRMSarritelDbContext context)
        {
            _context = context;
        }

        public class SyncUsuarioDto
        {
            [JsonPropertyName("usuarioId")]
            public int UsuarioId { get; set; }
            
            [JsonPropertyName("esManager")]
            public bool EsManager { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipos()
        {
            var equipos = await _context.Equipos
                .Include(e => e.UsuarioEquipos)
                .ThenInclude(ue => ue.Usuario)
                .ToListAsync();
            return Ok(equipos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipo(int id)
        {
            var equipo = await _context.Equipos
                .Include(e => e.UsuarioEquipos)
                .ThenInclude(ue => ue.Usuario)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipo == null) return NotFound();
            return Ok(equipo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Equipo equipo)
        {
            if (equipo == null) return BadRequest();
            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEquipo), new { id = equipo.Id }, equipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Equipo equipo)
        {
            if (id != equipo.Id) return BadRequest("ID mismatch");
            
            _context.Entry(equipo).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Equipos.AnyAsync(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null) return NotFound();

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/Usuarios")]
        public async Task<IActionResult> SyncUsuarios(int id, [FromBody] List<SyncUsuarioDto> participantes)
        {
            if (participantes == null) return BadRequest("The participants list cannot be null.");

            var equipo = await _context.Equipos
                .Include(e => e.UsuarioEquipos)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipo == null) return NotFound();

            // 1. Eliminar los existentes
            _context.UsuarioEquipos.RemoveRange(equipo.UsuarioEquipos);
            await _context.SaveChangesAsync();

            // 2. Agregar los nuevos
            foreach (var part in participantes)
            {
                if (part.UsuarioId <= 0) continue;
                
                var ue = new UsuarioEquipo { 
                    EquipoId = id, 
                    UsuarioId = part.UsuarioId, 
                    EsManager = part.EsManager,
                    FechaAsignacion = System.DateTime.UtcNow 
                };
                _context.UsuarioEquipos.Add(ue);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Sincronización completada." });
        }
    }
}
