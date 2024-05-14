using MailKit.Net.Smtp;
using MailKit.Security;
using Mambo.Email.Models;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Email.Concretes
{
    public sealed class EmailProvider(ILogger<EmailProvider> _logger, EmailSettings _emailSettings) : BaseEmailProvider
    {
        public override async Task SendEmailAsync(NewMail mail, CancellationToken cancellationToken)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_emailSettings.From, _emailSettings.From));
                emailMessage.To.AddRange(mail.To);
                if (mail.Cc != null) emailMessage.Cc.AddRange(mail.Cc);
                if (mail.Bcc != null) emailMessage.Bcc.AddRange(mail.Bcc);
                emailMessage.Subject = mail.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mail.Content };
                var builder = new BodyBuilder { HtmlBody = mail.Content };
                if (mail.Attachments != null)
                {
                    foreach (var attachment in mail.Attachments)
                    {
                        builder.Attachments.Add(attachment.FileName, attachment.FileContent);
                    }
                }
                emailMessage.Body = builder.ToMessageBody();

                _smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await _smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls, cancellationToken);

                await _smtpClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password, cancellationToken);

                await _smtpClient.SendAsync(emailMessage, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while sending email. Error: {@Exception}", ex);
                throw new Exception("An error occurred while sending email");
            }
            finally
            {
                await _smtpClient.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}