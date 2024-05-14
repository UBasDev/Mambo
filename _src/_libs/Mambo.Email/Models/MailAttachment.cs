using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Email.Models
{
    public struct MailAttachment
    {
        public MailAttachment()
        {
            FileName = string.Empty;
            FileContent = new byte[] { };
        }

        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}