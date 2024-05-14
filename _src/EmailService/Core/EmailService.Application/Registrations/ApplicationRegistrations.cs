using EmailService.Application.Consumers;
using Mambo.Email.Concretes;
using Mambo.Email.Models;
using Mambo.MassTransit.Concretes;
using Mambo.MassTransit.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Registrations
{
    public static class ApplicationRegistrations
    {
        public static void AddApplicationRegistrations(this IServiceCollection services, ConsumerMassTransitSettings massTransitSettings, EmailSettings emailSettings)
        {
            #region MassTransit

            services.AddEventBusForConsumers<SendEmailToUserConsumer>(massTransitSettings);

            #endregion MassTransit

            #region Email

            services.AddSingleton(emailSettings);
            services.AddSingleton<EmailProvider>();

            #endregion Email
        }
    }
}