using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Abstracts
{
    public interface IMongoGenericWriteRepository<TEntity>
    {
        Task CreateSingleDocumentAsync(TEntity document, CancellationToken cancellationToken);

        Task CreateMultipleDocumentsAsync(IEnumerable<TEntity> documents, CancellationToken cancellationToken);

        Task DeleteMultipleDocumentsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken);

        Task UpdateSingleDocumentsAsync(Expression<Func<TEntity, bool>> condition, UpdateDefinition<TEntity> updatedEntity, CancellationToken cancellationToken);
    }
}