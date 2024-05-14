using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.MassTransit.Contracts.Events.Abstracts
{
    public interface ISendEmailToUserEvent
    {
        public string Email { get; }
        public string UserId { get; }
    }
}