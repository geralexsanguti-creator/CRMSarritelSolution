using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using CRMSarritelApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(IUsuarioRepository usrepo, CRMSarritelDbContext context) : ControllerBase
    {
        private readonly IUsuarioRepository _repo = usrepo;
        private readonly CRMSarritelDbContext _context = context;
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _context.Usuarios
                .AsNoTracking()
                .AsSplitQuery()
                .Include(u => u.UsuarioRoles)
                    .ThenInclude(ur => ur.Rol)
                .Include(u => u.UsuarioEquipos)
                .ToListAsync();

            foreach (var u in usuarios)
            {
                var principalRole = u.UsuarioRoles.FirstOrDefault();
                u.RolId = principalRole?.RolId ?? 0;
                u.Rol_Nombre = principalRole?.Rol?.Nombre;
                u.EquipoIds = u.UsuarioEquipos.Select(ue => ue.EquipoId).Distinct().ToList();
            }

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .AsSplitQuery()
                .Include(u => u.UsuarioRoles)
                    .ThenInclude(ur => ur.Rol)
                .Include(u => u.UsuarioEquipos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null) return NotFound();

            var principalRole = usuario.UsuarioRoles.FirstOrDefault();
            usuario.RolId = principalRole?.RolId ?? 0;
            usuario.Rol_Nombre = principalRole?.Rol?.Nombre;
            usuario.EquipoIds = usuario.UsuarioEquipos.Select(ue => ue.EquipoId).Distinct().ToList();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CRMSarritelApi.DTOs.UsuarioUpdateDto dto)
        {
            Console.WriteLine($"DEBUG: Post User Username={dto.Username}, RolId={dto.RolId}");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine($"DEBUG: ModelState errors: {string.Join(", ", errors)}");
                return BadRequest(new { message = "Datos de usuario inválidos.", errors });
            }

            try 
            {
                var usuario = new Usuario
                {
                    Nombre = dto.Nombre ?? "",
                    Username = dto.Username ?? "",
                    Email = dto.Email,
                    Activo = dto.Activo,
                    Departamento = dto.Departamento,
                    Puesto = dto.Puesto,
                    FechaContratacion = dto.FechaContratacion?.ToUniversalTime(),
                    SalarioBase = dto.SalarioBase ?? 0,
                    Comisiones = dto.Comisiones ?? 0,
                    FotoPerfil = dto.FotoPerfil,
                    FechaCreation = DateTime.UtcNow
                };

                // Hashear contraseña con PasswordHasher (Unificado con AuthService)
                usuario.PasswordHash = _passwordHasher.HashPassword(usuario, dto.Password ?? "Sarritel123!");
                
                // Roles únicos
                if (dto.RolId > 0)
                {
                    usuario.UsuarioRoles.Add(new UsuarioRol { RolId = dto.RolId, FechaAsignacion = DateTime.UtcNow });
                }

                // Equipos únicos
                if (dto.EquipoIds != null)
                {
                    foreach (var eqId in dto.EquipoIds.Distinct())
                    {
                        usuario.UsuarioEquipos.Add(new UsuarioEquipo { EquipoId = eqId, FechaAsignacion = DateTime.UtcNow });
                    }
                }

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                Console.WriteLine($"DEBUG: User {usuario.Id} created successfully.");
                
                return await Get(usuario.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Error creating user: {ex.Message}");
                return StatusCode(500, new { message = "Error interno al crear usuario", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CRMSarritelApi.DTOs.UsuarioUpdateDto dto)
        {
            Console.WriteLine($"DEBUG: Put User id={id}, dto.Id={dto.Id}, dto.RolId={dto.RolId}");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine($"DEBUG: ModelState errors: {string.Join(", ", errors)}");
                return BadRequest(new { message = "Datos de actualización inválidos.", errors });
            }

            if (dto.Id != 0 && id != dto.Id)
            {
                return BadRequest(new { message = "ID de URL no coincide con ID del cuerpo." });
            }

            try 
            {
                var existingUser = await _context.Usuarios
                    .Include(u => u.UsuarioRoles)
                    .Include(u => u.UsuarioEquipos)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (existingUser == null) return NotFound(new { message = "Usuario no encontrado." });

                existingUser.Nombre = dto.Nombre;
                existingUser.Email = dto.Email;
                existingUser.Username = dto.Username;
                existingUser.Activo = dto.Activo;
                existingUser.Departamento = dto.Departamento;
                existingUser.Puesto = dto.Puesto;
                existingUser.FechaContratacion = dto.FechaContratacion?.ToUniversalTime();
                existingUser.SalarioBase = dto.SalarioBase;
                existingUser.Comisiones = dto.Comisiones;
                existingUser.FotoPerfil = dto.FotoPerfil;

                if (!string.IsNullOrEmpty(dto.Password))
                {
                    existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, dto.Password);
                }

                // Sincronizar Roles - Modo Explícito
                if (dto.RolId > 0)
                {
                    // Eliminar otros roles
                    var rolesToRemove = existingUser.UsuarioRoles.Where(r => r.RolId != dto.RolId).ToList();
                    foreach (var r in rolesToRemove)
                    {
                        _context.UsuarioRoles.Remove(r);
                        existingUser.UsuarioRoles.Remove(r);
                    }

                    // Añadir si no existe
                    if (!existingUser.UsuarioRoles.Any(r => r.RolId == dto.RolId))
                    {
                        existingUser.UsuarioRoles.Add(new UsuarioRol { UsuarioId = id, RolId = dto.RolId, FechaAsignacion = DateTime.UtcNow });
                    }
                }
                else 
                {
                    // Si RolId es 0 o negativo, ¿limpiamos todos?
                    var allRoles = existingUser.UsuarioRoles.ToList();
                    foreach (var r in allRoles)
                    {
                        _context.UsuarioRoles.Remove(r);
                        existingUser.UsuarioRoles.Remove(r);
                    }
                }

                // Sincronizar Equipos
                var requestedEquipos = (dto.EquipoIds ?? new List<int>()).Distinct().ToList();
                var currentEquipos = existingUser.UsuarioEquipos.ToList();

                foreach (var ce in currentEquipos)
                {
                    if (!requestedEquipos.Contains(ce.EquipoId))
                    {
                        _context.UsuarioEquipos.Remove(ce);
                        existingUser.UsuarioEquipos.Remove(ce);
                    }
                }

                foreach (var eqId in requestedEquipos)
                {
                    if (!currentEquipos.Any(ce => ce.EquipoId == eqId))
                    {
                        existingUser.UsuarioEquipos.Add(new UsuarioEquipo { UsuarioId = id, EquipoId = eqId, FechaAsignacion = DateTime.UtcNow });
                    }
                }

                await _context.SaveChangesAsync();
                Console.WriteLine($"DEBUG: User {id} updated successfully.");
                
                return await Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Error updating user: {ex.Message}");
                return StatusCode(500, new { message = "Error interno al actualizar usuario", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DuroBorrado(id);
            if (!success) return NotFound();
            
            return Ok(new { id });
        }
    }
}
