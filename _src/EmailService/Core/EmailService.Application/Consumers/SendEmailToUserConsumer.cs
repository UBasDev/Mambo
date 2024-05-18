using EmailService.Application.Repositories.SentMail;
using EmailService.Domain;
using Mambo.Email.Concretes;
using Mambo.Email.Models;
using Mambo.MassTransit.Contracts.Events.Abstracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;

namespace EmailService.Application.Consumers
{
    public sealed class SendEmailToUserConsumer(ILogger<SendEmailToUserConsumer> _logger, ISentMailWriteRepository _sentMailWriteRepository, EmailProvider _emailProvider) : IConsumer<ISendEmailToUserEvent>
    {
        private const string _testEmailAdress = "ugurcan.bas.dev@outlook.com";
        private const string _mailReason = "SignIn";
        private const string _mailSubject = "Mambo Security Warning";

        public async Task Consume(ConsumeContext<ISendEmailToUserEvent> context)
        {
            var messageFromQueue = context.Message;
            var mailContent = $"""
            <div style="text-align:center;">
                <h2>You've just logged in to Mambo.</h2>
                <h3><b>{messageFromQueue.Email}</b></h3>
                <h3>If this action belongs you, no need to worry. If this action doesn't belong to you, please report us. We will help you to secure your account.</h3>
            </div>
            """;
            try
            {
                await _emailProvider.SendEmailAsync(new CustomMail(new HashSet<string>() { messageFromQueue.Email }, _mailSubject, mailContent), context.CancellationToken);
                await _sentMailWriteRepository.CreateSingleDocumentAsync(SentMailEntity.CreateSentMail(messageFromQueue.UserId, messageFromQueue.Email, _mailReason), context.CancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while sending email or saving mail record to database. Message: {@Message}. Error: {@ErrorMessage}", messageFromQueue.ToString(), ex);
            }
        }
    }
}