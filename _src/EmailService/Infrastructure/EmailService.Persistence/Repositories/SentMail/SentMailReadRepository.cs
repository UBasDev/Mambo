using EmailService.Application.Repositories.SentMail;
using EmailService.Domain;
using Mambo.Mongo.Concretes;
using Mambo.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Persistence.Repositories.SentMail
{
    public sealed class SentMailReadRepository(MongoDbSettings mongoDbSettings) : MongoGenericReadRepository<SentMailEntity>(mongoDbSettings, _collectionName), ISentMailReadRepository
    {
        private const string _collectionName = "SentMails";
    }
}