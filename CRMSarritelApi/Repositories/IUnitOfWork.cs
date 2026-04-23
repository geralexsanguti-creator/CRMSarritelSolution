using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRMSarritelApi.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T, Tid> Repository<T, Tid>() where T : BaseEntity<Tid> where Tid : IEquatable<Tid>;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
    }
}
