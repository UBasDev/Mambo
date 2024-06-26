﻿using System.Linq.Expressions;

namespace CoreService.Application.Repositories.GenericRepository
{
    public interface IGenericReadRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        IEnumerable<TEntity> GetAllAsNoTracking();

        Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken);

        TEntity? GetSingleById(object id);

        Task<TEntity?> GetSingleByIdAsync(object id);

        TEntity? GetSingleByIdAsNoTracking(object id);

        Task<TEntity?> GetSingleByIdAsNoTrackingAsync(object id);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindByConditionAsNoTracking(Expression<Func<TEntity, bool>> predicate);
    }
}