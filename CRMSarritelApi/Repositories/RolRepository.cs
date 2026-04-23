using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMSarritelApi.Repositories
{
    public class RolRepository : GenericRepository<Rol, int>, IRolRepository
    {
        private readonly CRMSarritelDbContext _context;

        public RolRepository(CRMSarritelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permiso>> GetPermissionsByRolIdAsync(int rolId)
        {
            return await _context.RolPermisos
                .Where(rp => rp.RolId == rolId)
                .Include(rp => rp.Permiso)
                .Select(rp => rp.Permiso)
                .ToListAsync();
        }

        public async Task UpdateRolPermissionsAsync(int rolId, IEnumerable<int> permissionIds)
        {
            var existing = _context.RolPermisos.Where(rp => rp.RolId == rolId);
            _context.RolPermisos.RemoveRange(existing);

            foreach (var pId in permissionIds)
            {
                _context.RolPermisos.Add(new RolPermiso 
                { 
                    RolId = rolId, 
                    PermisoId = pId,
                    FechaAsignacion = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Permiso>> GetAllPermissionsAsync()
        {
            return await _context.Permisos.ToListAsync();
        }
    }
}
