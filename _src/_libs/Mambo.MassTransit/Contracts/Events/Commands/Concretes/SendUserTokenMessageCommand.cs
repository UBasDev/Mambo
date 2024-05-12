using Mambo.MassTransit.Contracts.Events.Commands.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.MassTransit.Contracts.Events.Commands.Concretes
{
    public class SendUserTokenMessageCommand : ISendUserTokenMessageCommand
    {
        public SendUserTokenMessageCommand()
        {
            UserId = string.Empty;
            AccessToken = string.Empty;
            AccessTokenExpireDate = 0;
            RefreshToken = string.Empty;
            RefreshTokenExpireDate = 0;
        }

        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public UInt64 AccessTokenExpireDate { get; set; }
        public string RefreshToken { get; set; }
        public UInt64 RefreshTokenExpireDate { get; set; }
    }
}