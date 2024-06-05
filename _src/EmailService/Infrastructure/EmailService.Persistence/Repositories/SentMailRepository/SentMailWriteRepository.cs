using EmailService.Application.Repositories.SentMailRepository;
using EmailService.Domain;
using Mambo.Mongo.Concretes;
using Mambo.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Persistence.Repositories.SentMailRepository
{
    public sealed class SentMailWriteRepository(MongoDbContext _mongoDbContext) : MongoGenericWriteRepository<SentMailEntity>(_mongoDbContext, _collectionName), ISentMailWriteRepository
    {
        private const string _collectionName = "SentMails";
    }
}