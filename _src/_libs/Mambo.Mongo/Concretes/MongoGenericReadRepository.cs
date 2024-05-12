using Amazon.Auth.AccessControlPolicy;
using Mambo.Mongo.Abstracts;
using Mambo.Mongo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Concretes
{
    public class MongoGenericReadRepository<TEntity> : MongoConnectionProvider, IGenericMongoReadRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoGenericReadRepository(MongoDbSettings mongoDbSettings, string collectionName) : base(mongoDbSettings)
        {
            _collection = _mongoDb.GetCollection<TEntity>(collectionName, new MongoCollectionSettings() { });
        }

        public async Task<IEnumerable<TEntity>> GetAllDocumentsAsync(CancellationToken cancellationToken)
        {
            return await (await _collection.FindAsync(_ => true, options: null, cancellationToken: cancellationToken)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetDocumentsByConditionAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        {
            return await (await _collection.FindAsync(condition, null, cancellationToken)).ToListAsync(cancellationToken);
        }
    }
}