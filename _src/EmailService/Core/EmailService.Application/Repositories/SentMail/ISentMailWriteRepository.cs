using EmailService.Domain;
using Mambo.Mongo.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Repositories.SentMail
{
    public interface ISentMailWriteRepository : IGenericMongoWriteRepository<SentMailEntity>
    {
    }
}