using Mambo.MassTransit.Models;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.MassTransit.Concretes
{
    public static class AddEventBusForConsumersProvider
    {
        public static void AddEventBusForConsumers<T>(this IServiceCollection services, ConsumerMassTransitSettings massTransitSettings) where T : IConsumer
        {
            try
            {
                services.AddMassTransit(massTransitOptions =>
                {
                    massTransitOptions.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: massTransitSettings.Prefix, includeNamespace: false));
                    massTransitOptions.AddConsumer(typeof(T));
                    massTransitOptions.UsingRabbitMq((context, rabbitMqConfig) =>
                    {
                        rabbitMqConfig.UseMessageRetry(r => r.Immediate(massTransitSettings.MessageRetry));
                        rabbitMqConfig.UseRateLimit(massTransitSettings.RateLimit, TimeSpan.FromSeconds(massTransitSettings.RateLimitInterval));

                        rabbitMqConfig.UseCircuitBreaker(cbConfiguration =>
                        {
                            cbConfiguration.TripThreshold = massTransitSettings.TripThreshold;
                            cbConfiguration.ActiveThreshold = massTransitSettings.ActiveThreshold;
                            cbConfiguration.TrackingPeriod = TimeSpan.FromMinutes(massTransitSettings.TrackingPeriod);
                            cbConfiguration.ResetInterval = TimeSpan.FromMinutes(massTransitSettings.ResetInterval);
                        });

                        rabbitMqConfig.Host(
                            host: $"amqp://{massTransitSettings.Username}:{massTransitSettings.Password}@{massTransitSettings.Host}:{massTransitSettings.Port}",
                            hostConfiguration =>
                            {
                                hostConfiguration.Username(massTransitSettings.Username);
                                hostConfiguration.Password(massTransitSettings.Password);
                            });

                        rabbitMqConfig.ReceiveEndpoint(
                            queueName: massTransitSettings.QueueName,
                            configureEndpoint =>
                            {
                                configureEndpoint.ConfigureConsumer(context, typeof(T));
                            });
                        rabbitMqConfig.ConfigureEndpoints(context);
                    });
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while configuring consumer event bus. The error: {ex.Message}");
            }
        }
    }
}