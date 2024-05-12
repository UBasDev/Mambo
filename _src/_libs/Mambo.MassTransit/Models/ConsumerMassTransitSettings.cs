using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.MassTransit.Models
{
    public class ConsumerMassTransitSettings
    {
        public ConsumerMassTransitSettings()
        {
            Prefix = string.Empty;
            Host = string.Empty;
            Port = 0;
            Username = string.Empty;
            Password = string.Empty;
            MessageRetry = 0;
            RateLimit = 0;
            RateLimitInterval = 0;
            TripThreshold = 0;
            ActiveThreshold = 0;
            TrackingPeriod = 0;
            ResetInterval = 0;
            QueueName = string.Empty;
        }

        public string Prefix { get; set; }
        public string Host { get; set; }
        public UInt16 Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte MessageRetry { get; set; }
        public byte RateLimit { get; set; }
        public byte RateLimitInterval { get; set; }
        public byte TripThreshold { get; set; }
        public byte ActiveThreshold { get; set; }
        public byte TrackingPeriod { get; set; }
        public byte ResetInterval { get; set; }
        public string QueueName { get; set; }
    }
}