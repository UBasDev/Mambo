using Mambo.Mongo.Concretes;
using Mambo.Mongo.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Repositories.TokenRepository;
using TokenService.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TokenService.Persistence.Repositories.TokenRepository
{
    public sealed class TokenReadRepository(MongoDbContext _mongoDbContext) : MongoGenericReadRepository<TokenEntity>(_mongoDbContext, _collectionName), ITokenReadRepository
    {
        private const string _collectionName = "Tokens";
    }
}