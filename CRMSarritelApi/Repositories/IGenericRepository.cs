using CRMSarritelApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Repositories
{
    public interface IGenericRepository<T, Tid> 
    {
        Task<T> Insertar(T value);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Tid value);
        IQueryable<T> GetAll();
        void Actualizar(T value);
        Task<bool> SuaveBorrado(Tid value);
        Task<bool> DuroBorrado(Tid value);
        Task<int> SaveChangesAsync();
    }
}
