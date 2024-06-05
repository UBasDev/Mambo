using Mambo.Mongo.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Domain;

namespace TokenService.Application.Repositories.TokenRepository
{
    public interface ITokenWriteRepository : IMongoGenericWriteRepository<TokenEntity>
    {
    }
}