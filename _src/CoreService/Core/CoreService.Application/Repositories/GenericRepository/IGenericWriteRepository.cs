﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Repositories.GenericRepository
{
    public interface IGenericWriteRepository<in TEntity>
    {
        void InsertSingle(TEntity entity);

        Task InsertSingleAsync(TEntity entity, CancellationToken cancellationToken);

        void InsertRange(IEnumerable<TEntity> entities);

        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        void DeleteSingle(TEntity entityToDelete);

        void DeleteSingleById(object id);

        Task DeleteSingleByIdAsync(object id, CancellationToken cancellationToken);

        void DeleteRange(IEnumerable<TEntity> entitiesToDelete);

        void UpdateSingle(TEntity entityToUpdate);

        void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    }
}