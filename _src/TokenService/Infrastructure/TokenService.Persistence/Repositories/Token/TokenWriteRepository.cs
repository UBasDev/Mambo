using Mambo.Mongo.Concretes;
using Mambo.Mongo.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Repositories.Token;
using TokenService.Domain;

namespace TokenService.Persistence.Repositories.Token
{
    public sealed class TokenWriteRepository(MongoDbSettings mongoDbSettings) : MongoGenericWriteRepository<TokenEntity>(mongoDbSettings, _collectionName), ITokenWriteRepository
    {
        private const string _collectionName = "Tokens";
    }
}