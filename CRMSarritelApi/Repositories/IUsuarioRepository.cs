using CRMSarritelApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario, int>
    {
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<Usuario?> GetByUsernameWithRolesAsync(string username);
    }

}
