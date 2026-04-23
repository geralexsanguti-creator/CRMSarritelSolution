using CRMSarritelApi.Data;
using CRMSarritelApi.DTOs;
using CRMSarritelApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRMSarritelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FichajesController : ControllerBase
    {
        private readonly CRMSarritelDbContext _context;

        public FichajesController(CRMSarritelDbContext context)
        {
            _context = context;
        }

        // GET: api/Fichajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FichajeDto>>> GetFichajes([FromQuery] int? usuarioId, [FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin)
        {
            var query = _context.Fichajes.Include(f => f.Usuario).AsQueryable();

            // Filtrar por ID de usuario si se proporciona
            if (usuarioId.HasValue)
            {
                query = query.Where(f => f.UsuarioId == usuarioId.Value);
            }
            // Si el usuario no es Admin/SuperAdmin, solo puede ver sus propios fichajes
            else
            {
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out int currentUserId))
                {
                    query = query.Where(f => f.UsuarioId == currentUserId);
                }
                else
                {
                    return Forbid();
                }
            }
            }

            if (fechaInicio.HasValue)
            {
                query = query.Where(f => f.HoraEntrada >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                // Incluir todo el día de la fecha de fin
                var fechaFinCierre = fechaFin.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(f => f.HoraEntrada <= fechaFinCierre);
            }

            var fichajesEntities = await query
                .OrderByDescending(f => f.HoraEntrada)
                .Include(f => f.Pausas)
                .ToListAsync();

            var fichajes = fichajesEntities.Select(f => new FichajeDto
                {
                    Id = f.Id,
                    UsuarioId = f.UsuarioId,
                    UsuarioNombre = f.Usuario != null ? f.Usuario.Nombre : null,
                    HoraEntrada = f.HoraEntrada,
                    HoraSalida = f.HoraSalida,
                    TipoRegistro = f.TipoRegistro,
                    HorasExtra = f.HorasExtra,
                    Notas = f.Notas,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt,
                    IsPausado = f.Pausas.Any(p => p.HoraFin == null),
                    TotalPausasMinutos = f.Pausas
                        .Where(p => p.HoraFin != null)
                        .Sum(p => (p.HoraFin!.Value - p.HoraInicio).TotalMinutes),
                    Pausas = f.Pausas.Select(p => new PausaDto {
                        Id = p.Id,
                        HoraInicio = p.HoraInicio,
                        HoraFin = p.HoraFin,
                        Motivo = p.Motivo
                    }).ToList()
                })
                .ToList();

            return Ok(fichajes);
        }

        // GET: api/Fichajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FichajeDto>> GetFichaje(int id)
        {
            var fichaje = await _context.Fichajes
                .Include(f => f.Usuario)
                .Where(f => f.Id == id)
                .Select(f => new FichajeDto
                {
                    Id = f.Id,
                    UsuarioId = f.UsuarioId,
                    UsuarioNombre = f.Usuario != null ? f.Usuario.Nombre : null,
                    HoraEntrada = f.HoraEntrada,
                    HoraSalida = f.HoraSalida,
                    TipoRegistro = f.TipoRegistro,
                    HorasExtra = f.HorasExtra,
                    Notas = f.Notas,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (fichaje == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out int currentUserId) && currentUserId != fichaje.UsuarioId)
                {
                    return Forbid();
                }
            }

            return Ok(fichaje);
        }

        // POST: api/Fichajes/Start
        // Inicia el cronómetro (HoraEntrada)
        [HttpPost("Start")]
        public async Task<ActionResult<FichajeDto>> StartFichaje([FromBody] FichajeCreateDto request)
        {
            // Validar que no tenga un fichaje abierto (sin salida) SOLO si es un turno normal
            if (request.TipoRegistro == "Trabajando" || string.IsNullOrEmpty(request.TipoRegistro))
            {
                var fichajeAbierto = await _context.Fichajes
                    .Where(f => f.UsuarioId == request.UsuarioId && f.HoraSalida == null)
                    .FirstOrDefaultAsync();

                if (fichajeAbierto != null)
                {
                    return BadRequest(new { message = "El usuario ya tiene un turno abierto." });
                }
            }

            // Validar si es el mismo usuario o si es admin
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out int currentUserId) && currentUserId != request.UsuarioId)
                {
                    return Forbid();
                }
            }

            var nuevoFichaje = new Fichaje
            {
                UsuarioId = request.UsuarioId,
                HoraEntrada = request.HoraEntrada ?? DateTime.UtcNow,
                HoraSalida = (request.TipoRegistro != "Trabajando" && request.TipoRegistro != null) ? request.HoraSalida : null,
                TipoRegistro = request.TipoRegistro ?? "Trabajando",
                Notas = request.Notas,
                CreatedAt = DateTime.UtcNow
            };

            _context.Fichajes.Add(nuevoFichaje);
            await _context.SaveChangesAsync();

            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);

            var dto = new FichajeDto
            {
                Id = nuevoFichaje.Id,
                UsuarioId = nuevoFichaje.UsuarioId,
                UsuarioNombre = usuario?.Nombre,
                HoraEntrada = nuevoFichaje.HoraEntrada,
                HoraSalida = nuevoFichaje.HoraSalida,
                TipoRegistro = nuevoFichaje.TipoRegistro,
                Notas = nuevoFichaje.Notas,
                CreatedAt = nuevoFichaje.CreatedAt
            };

            return CreatedAtAction(nameof(GetFichaje), new { id = nuevoFichaje.Id }, dto);
        }

        // POST: api/Fichajes/Stop/5
        // Detiene el cronómetro (HoraSalida)
        [HttpPost("Stop/{id}")]
        public async Task<IActionResult> StopFichaje(int id)
        {
            var fichaje = await _context.Fichajes.FindAsync(id);

            if (fichaje == null)
            {
                return NotFound();
            }

            if (fichaje.HoraSalida != null)
            {
                return BadRequest(new { message = "Este turno ya ha sido cerrado." });
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out int currentUserId) && currentUserId != fichaje.UsuarioId)
                {
                    return Forbid();
                }
            }

            fichaje.HoraSalida = DateTime.UtcNow;
            fichaje.UpdatedAt = DateTime.UtcNow;

            // Calcular horas extras simples (si excede las 8 horas en un día)
            var duracion = fichaje.HoraSalida.Value - fichaje.HoraEntrada;
            if (duracion.TotalHours > 8)
            {
                fichaje.HorasExtra = Math.Round(duracion.TotalHours - 8, 2);
            }

            _context.Entry(fichaje).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Turno cerrado correctamente", horaSalida = fichaje.HoraSalida, horasExtra = fichaje.HorasExtra });
        }

        // PUT: api/Fichajes/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UpdateFichaje(int id, FichajeUpdateDto request)
        {
            var fichaje = await _context.Fichajes.FindAsync(id);

            if (fichaje == null)
            {
                return NotFound();
            }

            if (request.HoraSalida.HasValue)
            {
                fichaje.HoraSalida = request.HoraSalida.Value;
            }

            if (!string.IsNullOrEmpty(request.TipoRegistro))
            {
                fichaje.TipoRegistro = request.TipoRegistro;
            }

            if (request.HorasExtra.HasValue)
            {
                fichaje.HorasExtra = request.HorasExtra.Value;
            }

            if (request.Notas != null)
            {
                fichaje.Notas = request.Notas;
            }

            fichaje.UpdatedAt = DateTime.UtcNow;

            _context.Entry(fichaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fichajes/Pause/5
        [HttpPost("Pause/{id}")]
        public async Task<IActionResult> PauseFichaje(int id)
        {
            var fichaje = await _context.Fichajes.Include(f => f.Pausas).FirstOrDefaultAsync(f => f.Id == id);
            if (fichaje == null) return NotFound();

            if (fichaje.HoraSalida != null)
                return BadRequest(new { message = "No se puede pausar un turno cerrado." });

            if (fichaje.Pausas.Any(p => p.HoraFin == null))
                return BadRequest(new { message = "El turno ya está pausado." });

            // Validar permisos
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out int currentUserId) && currentUserId != fichaje.UsuarioId)
                {
                    return Forbid();
                }
            }

            var pausa = new Pausa
            {
                FichajeId = id,
                HoraInicio = DateTime.UtcNow
            };

            _context.Pausas.Add(pausa);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Turno pausado correctamente", horaInicio = pausa.HoraInicio });
        }

        // POST: api/Fichajes/Resume/5
        [HttpPost("Resume/{id}")]
        public async Task<IActionResult> ResumeFichaje(int id)
        {
            var fichaje = await _context.Fichajes.FindAsync(id);
            if (fichaje == null) return NotFound();

            var pausaAbierta = await _context.Pausas
                .Where(p => p.FichajeId == id && p.HoraFin == null)
                .OrderByDescending(p => p.HoraInicio)
                .FirstOrDefaultAsync();

            if (pausaAbierta == null)
                return BadRequest(new { message = "No hay una pausa activa para este turno." });

            // Validar permisos
            if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out int currentUserId) && currentUserId != fichaje.UsuarioId)
                {
                    return Forbid();
                }
            }

            pausaAbierta.HoraFin = DateTime.UtcNow;
            _context.Entry(pausaAbierta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Turno reanudado correctamente", horaFin = pausaAbierta.HoraFin });
        }

        // DELETE: api/Fichajes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteFichaje(int id)
        {
            var fichaje = await _context.Fichajes.FindAsync(id);
            if (fichaje == null)
            {
                return NotFound();
            }

            _context.Fichajes.Remove(fichaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FichajeExists(int id)
        {
            return _context.Fichajes.Any(e => e.Id == id);
        }
    }
}
