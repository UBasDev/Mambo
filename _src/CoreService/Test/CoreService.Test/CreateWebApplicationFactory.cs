using CoreService.Application.Contexts;
using CoreService.Application.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;

namespace CoreService.Test
{
    public class CreateWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer;

        public CreateWebApplicationFactory()
        {
            var configuration = CreateNewConfigurationBuilder();

            var appSettings = new AppSettings();
            configuration.Bind(nameof(AppSettings), appSettings);

            _dbContainer = new PostgreSqlBuilder()
        .WithImage(appSettings.PostgreSqlTestContainerSettings.ImageName)
        .WithDatabase(appSettings.PostgreSqlTestContainerSettings.DatabaseName)
        .WithUsername(appSettings.PostgreSqlTestContainerSettings.Username)
        .WithPassword(appSettings.PostgreSqlTestContainerSettings.Password)
        .WithCleanUp(appSettings.PostgreSqlTestContainerSettings.IsCleanUp)
        .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<MamboCoreDbContext>));

                services.AddDbContext<MamboCoreDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            using var scope = Services.CreateScope();
            var dbContext1 = scope.ServiceProvider.GetRequiredService<MamboCoreDbContext>();
            await dbContext1.Database.MigrateAsync();
        }

        public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }

        private static IConfigurationRoot CreateNewConfigurationBuilder() => new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"))
                .AddJsonFile(path: $"appsettings.Local.json", optional: false, reloadOnChange: true)
            .Build();
    }
}