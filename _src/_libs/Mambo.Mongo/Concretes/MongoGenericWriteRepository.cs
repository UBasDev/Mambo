using Mambo.Mongo.Abstracts;
using Mambo.Mongo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Concretes
{
    public class MongoGenericWriteRepository<TEntity> : MongoConnectionProvider, IGenericMongoWriteRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoGenericWriteRepository(MongoDbSettings mongoDbSettings, string collectionName) : base(mongoDbSettings)
        {
            _collection = _mongoDb.GetCollection<TEntity>(collectionName);
        }

        public async Task<(bool isSuccessful, string? errorMessage)> CreateMultipleDocumentsAsync(IEnumerable<TEntity> documents)
        {
            try
            {
                await _collection.InsertManyAsync(documents);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool isSuccessful, string? errorMessage)> CreateSingleDocumentAsync(TEntity document)
        {
            try
            {
                await _collection.InsertOneAsync(document);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}