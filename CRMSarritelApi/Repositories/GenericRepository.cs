using CRMSarritelApi.Data;
using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRMSarritelApi.Repositories
{
    public class GenericRepository<T, Tid> : IGenericRepository<T, Tid>
        where T : BaseEntity<Tid>
        where Tid : IEquatable<Tid>
    {
        private readonly CRMSarritelDbContext _dbContext;

        protected DbSet<T> _entities;

        public GenericRepository(CRMSarritelDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _entities = _dbContext.Set<T>();
        }
        public void Actualizar(T value)
        {
            _entities.Update(value);
        }

        public async Task<bool> DuroBorrado(Tid value)
        {
            T? entity = await GetByIdAsync(value);
            if (entity == null) return false;

            _entities.Remove(entity);
            await SaveChangesAsync();
            return true;    
        }

        public  IQueryable<T> GetAll() => _entities;
        

        public async Task<T?> GetByIdAsync(Tid id) => await _entities.FirstOrDefaultAsync(a => a.Id.Equals(id));
        

        public async Task<T> Insertar(T value)
        {
            var result = await _entities.AddAsync(value);
            return result.Entity;
        }

        public async Task<bool> SuaveBorrado(Tid value)
        {
            T? entity = await GetByIdAsync(value);
            if (entity == null) return false;
            
            _entities.Update(entity);
            return true;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
