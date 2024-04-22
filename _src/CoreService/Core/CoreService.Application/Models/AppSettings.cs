using Mambo.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Application.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            MamboCoreDbConnectionString = string.Empty;
            JwtSettings = new JwtSettings();
            ElasticsearchSettings = new ElasticsearchSettings();
        }

        public string MamboCoreDbConnectionString { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public ElasticsearchSettings ElasticsearchSettings { get; set; }
    }

    public class ElasticsearchSettings
    {
        public ElasticsearchSettings()
        {
            ElasticsearchUrl = string.Empty;
        }

        public string ElasticsearchUrl { get; set; }
    }
}