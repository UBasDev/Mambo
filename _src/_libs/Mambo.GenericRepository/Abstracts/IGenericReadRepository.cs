﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.GenericRepository.Abstracts
{
    public interface IGenericReadRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetAllAsNoTracking();

        Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync();

        TEntity? GetSingleById(object id);

        Task<TEntity?> GetSingleByIdAsync(object id);

        TEntity? GetSingleByIdAsNoTracking(object id);

        Task<TEntity?> GetSingleByIdAsNoTrackingAsync(object id);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindByConditionAsNoTracking(Expression<Func<TEntity, bool>> predicate);
    }
}