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

namespace TokenService.Persistence.Repositories.TokenRepository
{
    public sealed class TokenWriteRepository(MongoDbContext _mongoDbContext) : MongoGenericWriteRepository<TokenEntity>(_mongoDbContext, _collectionName), ITokenWriteRepository
    {
        private const string _collectionName = "Tokens";
    }
}