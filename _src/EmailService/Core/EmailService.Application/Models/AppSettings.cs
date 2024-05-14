using Mambo.Email.Models;
using Mambo.MassTransit.Models;
using Mambo.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Models
{
    public sealed class AppSettings
    {
        public AppSettings()
        {
            EmailSettings = new EmailSettings();
            ConsumerMassTransitSettings = new ConsumerMassTransitSettings();
            MongoDbSettings = new MongoDbSettings();
        }

        public EmailSettings EmailSettings { get; set; }
        public ConsumerMassTransitSettings ConsumerMassTransitSettings { get; set; }
        public MongoDbSettings MongoDbSettings { get; set; }
    }
}