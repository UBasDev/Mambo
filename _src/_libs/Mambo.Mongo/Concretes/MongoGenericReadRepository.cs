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
    public class MongoGenericReadRepository<TEntity>(MongoDbSettings mongoDbSettings, string collectionName) : MongoConnectionProvider(mongoDbSettings), IGenericMongoReadRepository<TEntity> where TEntity : class
    {
        private readonly string _collectionName = collectionName;

        public async Task<IEnumerable<TEntity>> GetAllDocumentsAsync(MongoCollectionSettings? collectionSettings = null)
        {
            var collectionData = _mongoDb.GetCollection<TEntity>(_collectionName, collectionSettings ?? new MongoCollectionSettings() { });
            return await (await collectionData.FindAsync(_ => true)).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetDocumentsByConditionAsync(Expression<Func<TEntity, bool>> condition, MongoCollectionSettings? collectionSettings = null)
        {
            var collectionData = _mongoDb.GetCollection<TEntity>(_collectionName, collectionSettings ?? new MongoCollectionSettings() { });
            return await (await collectionData.FindAsync(condition)).ToListAsync();
        }
    }
}