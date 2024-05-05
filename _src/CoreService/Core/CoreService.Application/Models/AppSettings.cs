﻿using Mambo.JWT;
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
            GenerateTokenSettings = new GenerateTokenSettings();
        }

        public string MamboCoreDbConnectionString { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public GenerateTokenSettings GenerateTokenSettings { get; set; }
        public ElasticsearchSettings ElasticsearchSettings { get; set; }
        public CorsOptions CorsOptions { get; set; }
    }

    public class ElasticsearchSettings
    {
        public ElasticsearchSettings()
        {
            ElasticsearchUrl = string.Empty;
        }

        public string ElasticsearchUrl { get; set; }
    }

    public class GenerateTokenSettings
    {
        public GenerateTokenSettings()
        {
            AccessTokenExpireTime = 0;
            RefreshTokenExpireTime = 0;
            SecretKey = string.Empty;
            Issuer = string.Empty;
            Audience = string.Empty;
        }

        public UInt16 AccessTokenExpireTime { get; set; }
        public UInt16 RefreshTokenExpireTime { get; set; }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class CorsOptions
    {
        public CorsOptions()
        {
            AllowedOrigins = [];
        }

        public string[] AllowedOrigins { get; set; }
    }
}