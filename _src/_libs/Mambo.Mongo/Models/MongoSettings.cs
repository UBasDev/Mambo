using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.Mongo.Models
{
    public class MongoDbSettings
    {
        public MongoDbSettings()
        {
            AuthDbName = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Hostname = string.Empty;
            Port = 0;
            ConnectTimeout = 0;
            QueueTimeout = 0;
            UseSSl = false;
            MinConnectionPoolSize = 0;
            MaxConnectionPoolSize = 0;
            DatabaseName = string.Empty;
            CheckCertificateRevocation = false;
        }

        public string AuthDbName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Hostname { get; set; }
        public UInt16 Port { get; set; }
        public UInt16 ConnectTimeout { get; set; }
        public UInt16 QueueTimeout { get; set; }
        public bool UseSSl { get; set; }
        public byte MinConnectionPoolSize { get; set; }
        public byte MaxConnectionPoolSize { get; set; }
        public string DatabaseName { get; set; }
        public bool CheckCertificateRevocation { get; set; }
    }
}