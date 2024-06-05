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
    public abstract class MongoGenericWriteRepository<TEntity>(MongoDbContext mongoDbContext, string collectionName) : IMongoGenericWriteRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection = mongoDbContext.GetCollectionByName<TEntity>(collectionName);

        public async Task CreateMultipleDocumentsAsync(IEnumerable<TEntity> documents, CancellationToken cancellationToken)
        {
            await _collection.InsertManyAsync(documents, null, cancellationToken);
        }

        public async Task CreateSingleDocumentAsync(TEntity document, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(document, null, cancellationToken);
        }

        public async Task DeleteMultipleDocumentsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        {
            await _collection.DeleteManyAsync(condition, null, cancellationToken);
        }

        public async Task UpdateSingleDocumentsAsync(Expression<Func<TEntity, bool>> condition, UpdateDefinition<TEntity> updatedEntity, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(condition, updatedEntity, null, cancellationToken);
        }
    }
}