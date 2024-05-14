using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.MassTransit.Contracts.Events.Commands.Abstracts
{
    public interface ISendUserTokenMessageCommand
    {
        public string UserId { get; }
        public string AccessToken { get; }
        public UInt64 AccessTokenExpireDate { get; }
        public string RefreshToken { get; }
        public UInt64 RefreshTokenExpireDate { get; }
        public string Email { get; }
    }
}