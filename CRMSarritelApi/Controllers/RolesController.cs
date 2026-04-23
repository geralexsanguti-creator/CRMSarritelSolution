using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolRepository _repo;

        public RolesController(IRolRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _repo.GetAll().ToListAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rol = await _repo.GetByIdAsync(id);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpGet("{id}/permissions")]
        public async Task<IActionResult> GetPermissions(int id)
        {
            var permissions = await _repo.GetPermissionsByRolIdAsync(id);
            return Ok(permissions);
        }

        [HttpPost("{id}/permissions")]
        public async Task<IActionResult> UpdatePermissions(int id, [FromBody] List<int> permissionIds)
        {
            await _repo.UpdateRolPermissionsAsync(id, permissionIds);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rol rol)
        {
            if (string.IsNullOrEmpty(rol.Nombre)) return BadRequest("El nombre del rol es requerido");
            var result = await _repo.Insertar(rol);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Rol rol)
        {
            if (id != rol.Id) return BadRequest();
            if (string.IsNullOrEmpty(rol.Nombre)) return BadRequest("El nombre del rol es requerido");
            
            _repo.Actualizar(rol);
            await _repo.SaveChangesAsync();
            return Ok(rol);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] CRMSarritelDbContext context)
        {
            // Proactive check for dependencies to provide better error messages
            var hasUsers = await context.UsuarioRoles.AnyAsync(ur => ur.RolId == id);
            if (hasUsers) return BadRequest("No se puede eliminar el rol porque tiene usuarios asignados.");

            var hasCommissionRules = await context.ReparticionesComision.AnyAsync(rc => rc.RolId == id);
            if (hasCommissionRules) return BadRequest("No se puede eliminar el rol porque está siendo utilizado en reglas de comisión.");

            try
            {
                var deleted = await _repo.DuroBorrado(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest("No se puede eliminar el rol debido a restricciones de integridad (probablemente esté vinculado a otros registros).");
            }
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _repo.GetAllPermissionsAsync();
            return Ok(permissions);
        }
    }
}