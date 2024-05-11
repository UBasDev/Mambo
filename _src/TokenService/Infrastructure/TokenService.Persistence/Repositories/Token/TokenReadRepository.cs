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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TokenService.Persistence.Repositories.Token
{
    public sealed class TokenReadRepository(MongoDbSettings mongoDbSettings) : MongoGenericReadRepository<TokenEntity>(mongoDbSettings, _collectionName), ITokenReadRepository
    {
        private const string _collectionName = "Tokens";
    }
}