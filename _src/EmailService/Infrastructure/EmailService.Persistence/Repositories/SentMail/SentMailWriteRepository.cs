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
    public sealed class SentMailWriteRepository(MongoDbSettings mongoDbSettings) : MongoGenericWriteRepository<SentMailEntity>(mongoDbSettings, _collectionName), ISentMailWriteRepository
    {
        private const string _collectionName = "SentMails";
    }
}