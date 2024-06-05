using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Abstracts
{
    public interface IMongoGenericReadRepository<TEntity>
    {
        Task<List<TEntity>> GetAllDocumentsAsync(CancellationToken cancellationToken);

        Task<List<TEntity>> GetDocumentsByConditionAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken);

        Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken);
    }
}