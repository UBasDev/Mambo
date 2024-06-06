using Mambo.MassTransit.Concretes;
using Mambo.MassTransit.Contracts.Events.Commands.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Helpers
{
    public sealed class LocalHelpers(PublisherEventBusProvider _eventBusProvider)
    {
        public async Task SendTokenWithRabbitMqMessage(string generatedAccessToken, UInt16 accessTokenExpireTime, UInt16 refreshTokenExpireTime, string generatedRefreshToken, string userId, string email, CancellationToken cancellationToken)
        {
            await _eventBusProvider._eventBus.Send<SendUserTokenMessageCommand>(new SendUserTokenMessageCommand()
            {
                AccessToken = generatedAccessToken,
                RefreshToken = generatedRefreshToken,
                UserId = userId,
                AccessTokenExpireDate = (UInt64)DateTimeOffset.UtcNow.AddMinutes(accessTokenExpireTime).ToUnixTimeSeconds(),
                RefreshTokenExpireDate = (UInt64)DateTimeOffset.UtcNow.AddMinutes(refreshTokenExpireTime).ToUnixTimeSeconds(),
                Email = email
            }, cancellationToken);
        }
    }
}