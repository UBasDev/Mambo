using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Abstracts
{
    public interface IGenericMongoReadRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllDocumentsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetDocumentsByConditionAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken);
    }
}