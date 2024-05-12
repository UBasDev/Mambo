using Mambo.MassTransit.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.MassTransit.Concretes
{
    public class PublisherEventBusProvider
    {
        public ISendEndpoint _eventBus { get; private set; }
        private readonly PublisherMassTransitSettings _massTransitSettings;

        public PublisherEventBusProvider(PublisherMassTransitSettings massTransitSettings)
        {
            _massTransitSettings = massTransitSettings;
            _eventBus = ConfigureEventBus();
        }

        private ISendEndpoint ConfigureEventBus()
        {
            try
            {
                var baseConnectionString = $"amqp://{_massTransitSettings.Username}:{_massTransitSettings.Password}@{_massTransitSettings.Host}:{_massTransitSettings.Port}";
                var bus = Bus.Factory.CreateUsingRabbitMq(configuration1 =>
                {
                    configuration1.Host(
                        host: baseConnectionString,
                        hostConfiguration =>
                        {
                            hostConfiguration.Username(_massTransitSettings.Username);
                            hostConfiguration.Password(_massTransitSettings.Password);
                        });
                    configuration1.UseMessageRetry(r => r.Immediate(_massTransitSettings.MessageRetry));

                    configuration1.UseRateLimit(_massTransitSettings.RateLimit, TimeSpan.FromSeconds(_massTransitSettings.RateLimitInterval));

                    configuration1.UseCircuitBreaker(cbConfiguration =>
                    {
                        cbConfiguration.TripThreshold = _massTransitSettings.TripThreshold;
                        cbConfiguration.ActiveThreshold = _massTransitSettings.ActiveThreshold;
                        cbConfiguration.TrackingPeriod = TimeSpan.FromMinutes(_massTransitSettings.TrackingPeriod);
                        cbConfiguration.ResetInterval = TimeSpan.FromMinutes(_massTransitSettings.ResetInterval);
                    });
                });
                return bus.GetSendEndpoint(new Uri(baseConnectionString + $"/{_massTransitSettings.QueueName}")).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while configuring publisher event bus. The error: {ex.Message}");
            }
        }
    }
}