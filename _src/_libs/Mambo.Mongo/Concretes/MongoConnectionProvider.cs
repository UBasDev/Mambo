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
    public abstract class MongoConnectionProvider
    {
        protected IMongoDatabase _mongoDb { get; private set; }

        protected MongoConnectionProvider(MongoDbSettings mongoDbSettings)
        {
            var mongoClientSettings = new MongoClientSettings()
            {
                Credential = MongoCredential.CreateCredential(mongoDbSettings.AuthDbName, mongoDbSettings.Username, mongoDbSettings.Password),

                Server = new MongoServerAddress(mongoDbSettings.Hostname, mongoDbSettings.Port),

                ConnectTimeout = TimeSpan.FromSeconds(mongoDbSettings.ConnectTimeout),

                WaitQueueTimeout = TimeSpan.FromSeconds(mongoDbSettings.QueueTimeout),

                UseSsl = mongoDbSettings.UseSSl,

                MinConnectionPoolSize = mongoDbSettings.MinConnectionPoolSize,

                MaxConnectionPoolSize = mongoDbSettings.MaxConnectionPoolSize,
                SslSettings = new SslSettings
                {
                    CheckCertificateRevocation = mongoDbSettings.CheckCertificateRevocation,
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                    ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true
                }
            };
            var client = new MongoClient(mongoClientSettings);
            _mongoDb = client.GetDatabase(mongoDbSettings.DatabaseName);
        }
    }
}