using Mambo.JWT;
using Mambo.MassTransit.Models;
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
            UiAdminUsername = string.Empty;
            UiAdminPassword = string.Empty;
            JwtSettings = new JwtSettings();
            ElasticsearchSettings = new ElasticsearchSettings();
            GenerateTokenSettings = new GenerateTokenSettings();
            CorsOptions = new CorsOptions();
            PostgreSqlTestContainerSettings = new PostgreSqlTestContainerSettings();
            PublisherMassTransitSettings = new PublisherMassTransitSettings();
        }

        public string UiAdminUsername { get; set; }
        public string UiAdminPassword { get; set; }
        public string MamboCoreDbConnectionString { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public GenerateTokenSettings GenerateTokenSettings { get; set; }
        public ElasticsearchSettings ElasticsearchSettings { get; set; }
        public CorsOptions CorsOptions { get; set; }
        public PostgreSqlTestContainerSettings PostgreSqlTestContainerSettings { get; set; }
        public PublisherMassTransitSettings PublisherMassTransitSettings { get; set; }
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

    public class PostgreSqlTestContainerSettings
    {
        public PostgreSqlTestContainerSettings()
        {
            Username = string.Empty;
            Password = string.Empty;
            DatabaseName = string.Empty;
            ImageName = string.Empty;
            IsCleanUp = false;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string ImageName { get; set; }
        public bool IsCleanUp { get; set; }
    }
}