using Mambo.MassTransit.Models;
using Mambo.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService.Application.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            MongoDbSettings = new MongoDbSettings();
            ConsumerMassTransitSettings = new ConsumerMassTransitSettings();
        }

        public MongoDbSettings MongoDbSettings { get; set; }
        public ConsumerMassTransitSettings ConsumerMassTransitSettings { get; set; }
    }
}