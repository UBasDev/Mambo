using CoreService.Application.Contexts;
using CoreService.Application.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreService.Persistence.Repositories.GenericRepository
{
    public abstract class GenericReadRepository<TEntity>(MamboCoreDbContext dbContext) : IGenericReadRepository<TEntity> where TEntity : class
    {
        protected readonly MamboCoreDbContext _dbContext = dbContext;
        private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public virtual IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);

        public virtual IQueryable<TEntity> FindByConditionAsNoTracking(Expression<Func<TEntity, bool>> predicate) => _dbSet.AsNoTracking().Where(predicate);

        public virtual IEnumerable<TEntity> GetAll() => _dbSet.ToList();

        public virtual IEnumerable<TEntity> GetAllAsNoTracking() => _dbSet.AsNoTracking().ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken) => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) => await _dbSet.ToListAsync(cancellationToken);

        public virtual TEntity? GetSingleById(object id) => _dbSet.Find(id);

        public virtual TEntity? GetSingleByIdAsNoTracking(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return null;
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<TEntity?> GetSingleByIdAsNoTrackingAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<TEntity?> GetSingleByIdAsync(object id) => await _dbSet.FindAsync(id);
    }
}