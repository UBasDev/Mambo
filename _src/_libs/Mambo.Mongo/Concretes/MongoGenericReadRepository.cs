using Amazon.Auth.AccessControlPolicy;
using Mambo.Mongo.Abstracts;
using Mambo.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Concretes
{
    public abstract class MongoGenericReadRepository<TEntity>(MongoDbContext mongoDbContext, string collectionName) : IMongoGenericReadRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection = mongoDbContext.GetCollectionByName<TEntity>(collectionName);

        public async Task<List<TEntity>> GetAllDocumentsAsync(CancellationToken cancellationToken)
        {
            return await (await _collection.FindAsync(_ => true, options: null, cancellationToken: cancellationToken)).ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetDocumentsByConditionAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        {
            return await (await _collection.FindAsync(condition, null, cancellationToken)).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var result1 = await _collection.FindAsync(Builders<TEntity>.Filter.Eq("_id", new ObjectId(id)), cancellationToken: cancellationToken);
            return await result1.FirstOrDefaultAsync(cancellationToken);
        }
    }
}