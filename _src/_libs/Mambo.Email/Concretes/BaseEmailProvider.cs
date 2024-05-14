using MailKit.Net.Smtp;
using Mambo.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Email.Concretes
{
    public abstract class BaseEmailProvider : IDisposable
    {
        protected SmtpClient _smtpClient { get; set; }

        protected BaseEmailProvider()
        {
            _smtpClient = new SmtpClient();
        }

        public abstract Task SendEmailAsync(NewMail mail, CancellationToken cancellationToken);

        private bool _disposed = false;

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    _smtpClient.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}