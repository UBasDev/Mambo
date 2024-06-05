using Mambo.Mongo.Concretes;
using Mambo.Mongo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService.Application.Contexts
{
    public class MongoDbContext(MongoDbSettings _mongoDbSettings) : MongoConnectionProvider(_mongoDbSettings)
    {
        public IMongoCollection<T> GetCollectionByName<T>(string name)
        {
            return MongoDb.GetCollection<T>(name, new MongoCollectionSettings()
            {
                AssignIdOnInsert = true
            });
        }
    }
}