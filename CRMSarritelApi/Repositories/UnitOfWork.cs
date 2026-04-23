using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace CRMSarritelApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly ConcurrentDictionary<(Type EntityType, Type IdType), object> _repos = new();

        public UnitOfWork(CRMSarritelDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<T, Tid> Repository<T, Tid>()
            where T : BaseEntity<Tid>
            where Tid : IEquatable<Tid>
        {
            var key = (typeof(T), typeof(Tid));
            var repoObj = _repos.GetOrAdd(key, k => (object)new GenericRepository<T, Tid>((CRMSarritelDbContext)_dbContext));

            return (IGenericRepository<T, Tid>)repoObj;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
            _dbContext.SaveChangesAsync(ct);

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default) =>
            _dbContext.Database.BeginTransactionAsync(ct);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}