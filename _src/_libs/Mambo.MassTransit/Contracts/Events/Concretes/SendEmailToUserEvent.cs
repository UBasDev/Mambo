using Mambo.MassTransit.Contracts.Events.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.MassTransit.Contracts.Events.Concretes
{
    public sealed class SendEmailToUserEvent : ISendEmailToUserEvent
    {
        public SendEmailToUserEvent()
        {
            Email = string.Empty;
            UserId = string.Empty;
        }

        public string Email { get; set; }
        public string UserId { get; set; }
    }
}