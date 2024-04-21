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
        public string MamboCoreDbConnectionString { get; set; }
        public JwtSettings JwtSettings { get; set; }
    }
}