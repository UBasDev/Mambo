using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Email.Models
{
    public struct CustomMail
    {
        public ICollection<MailboxAddress> To { get; set; }
        public ICollection<MailboxAddress> Cc { get; set; }
        public ICollection<MailboxAddress> Bcc { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public ICollection<MailAttachment> Attachments { get; set; }

        public CustomMail(ICollection<string> to, string subject, string content, ICollection<MailAttachment> _attachments = null, ICollection<string> cc = null, ICollection<string> bcc = null)
        {
            To = new HashSet<MailboxAddress>();
            Cc = new HashSet<MailboxAddress>();
            Bcc = new HashSet<MailboxAddress>();
            foreach (var currentTo in to)
            {
                To.Add(new MailboxAddress(currentTo, currentTo));
            }
            Subject = subject;
            Content = content;
            Attachments = _attachments;
            if (cc != null)
            {
                foreach (var currentCc in cc)
                {
                    Cc.Add(new MailboxAddress(currentCc, currentCc));
                }
            }
            if (bcc != null)
            {
                foreach (var currentBcc in bcc)
                {
                    Bcc.Add(new MailboxAddress(currentBcc, currentBcc));
                }
            }
        }
    }
}