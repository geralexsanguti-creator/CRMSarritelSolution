using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRMSarritelApi.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario, int>, IUsuarioRepository
    {
        public UsuarioRepository(CRMSarritelDbContext context) : base(context)
        {
        }
        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            // Opción 1: más legible y común (recomendada)
            return await _entities
                .FirstOrDefaultAsync(u => u.Username == username);

            // Opción 2: si quieres ignorar mayúsculas/minúsculas (útil en algunos sistemas)
            // return await _entities
            //     .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Username, username));

            // Opción 3: si usas .NET 8+ y quieres más rendimiento en algunos casos
            // return await _entities
            //     .Where(u => u.Username == username)
            //     .Select(u => u) // o proyectar a DTO si lo necesitas
            //     .FirstOrDefaultAsync();
        }


        public async Task<bool> AnyAsync(string username)
        {
            return await _entities.AnyAsync(u => u.Username == username);
        }


        public async Task<Usuario?> GetByUsernameWithRolesAsync(string username)
        {
            return await _entities
                .Include(u => u.UsuarioEquipos)
                .Include(u => u.UsuarioRoles)
                    .ThenInclude(ur => ur.Rol)
                        .ThenInclude(r => r!.RolPermisos)
                            .ThenInclude(rp => rp.Permiso)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

    }

}
