﻿using Mambo.MassTransit.Contracts.Events.Abstracts;
using Mambo.MassTransit.Contracts.Events.Commands.Abstracts;
using Mambo.MassTransit.Contracts.Events.Concretes;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.Models;
using TokenService.Application.Repositories.TokenRepository;
using TokenService.Domain;

namespace TokenService.Application.Consumers
{
    public class SendUserTokenConsumer(ILogger<SendUserTokenConsumer> _logger, ITokenReadRepository _tokenReadRepository, ITokenWriteRepository _tokenWriteRepository, AppSettings _appSettings) : IConsumer<ISendUserTokenMessageCommand>
    {
        public async Task Consume(ConsumeContext<ISendUserTokenMessageCommand> context)
        {
            var messageFromQueue = context.Message;
            try
            {
                var userTokens = await _tokenReadRepository.GetDocumentsByConditionAsync(t => t.UserId == messageFromQueue.UserId, context.CancellationToken);
                if (userTokens.Count > 1) //If there are many tokens which belong to user in db
                {
                    await _tokenWriteRepository.DeleteMultipleDocumentsAsync(t => userTokens.Select(ut => ut.Id).ToList().Contains(t.Id), context.CancellationToken);

                    await CreateSingleTokenAsync(messageFromQueue, context.CancellationToken);
                }
                else if (userTokens.Count == 1) //If user already has just one token in db
                {
                    await _tokenWriteRepository.UpdateSingleDocumentsAsync(t => t.Id == userTokens[0].Id, Builders<TokenEntity>.Update.Set(t => t.AccessToken, messageFromQueue.AccessToken).Set(t => t.AccessTokenExpireDate, messageFromQueue.AccessTokenExpireDate).Set(t => t.RefreshToken, messageFromQueue.RefreshToken).Set(t => t.RefreshTokenExpireDate, messageFromQueue.RefreshTokenExpireDate).Set(t => t.UpdatedAt, DateTimeOffset.UtcNow), context.CancellationToken);
                }
                else //If there is no token exists
                {
                    await CreateSingleTokenAsync(messageFromQueue, context.CancellationToken);
                }
                if (_appSettings.IsEmailSendActive)
                {
                    await context.Publish<ISendEmailToUserEvent>(new SendEmailToUserEvent()
                    {
                        Email = messageFromQueue.Email,
                        UserId = messageFromQueue.UserId
                    }, context.CancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while saving user token to database. Message: {@Message}. Error: {@ErrorMessage}", messageFromQueue.ToString(), ex);
            }
        }

        private async Task CreateSingleTokenAsync(ISendUserTokenMessageCommand messageFromQueue, CancellationToken cancellationToken)
        {
            await _tokenWriteRepository.CreateSingleDocumentAsync(TokenEntity.CreateNewTokenEntity(messageFromQueue.UserId, messageFromQueue.AccessToken, messageFromQueue.AccessTokenExpireDate, messageFromQueue.RefreshToken, messageFromQueue.RefreshTokenExpireDate), cancellationToken);
        }
    }
}