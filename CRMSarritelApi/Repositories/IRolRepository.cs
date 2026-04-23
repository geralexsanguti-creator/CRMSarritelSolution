using CRMSarritelApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMSarritelApi.Repositories
{
    public interface IRolRepository : IGenericRepository<Rol, int>
    {
        Task<IEnumerable<Permiso>> GetPermissionsByRolIdAsync(int rolId);
        Task UpdateRolPermissionsAsync(int rolId, IEnumerable<int> permissionIds);
        Task<IEnumerable<Permiso>> GetAllPermissionsAsync();
    }
}
