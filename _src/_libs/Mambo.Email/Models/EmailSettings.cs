using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Email.Models
{
    public sealed class EmailSettings
    {
        public EmailSettings()
        {
            From = string.Empty;
            SmtpServer = string.Empty;
            Port = 0;
            UserName = string.Empty;
            Password = string.Empty;
        }

        public string From { get; set; }
        public string SmtpServer { get; set; }
        public UInt16 Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}