using EmailService.Application.Models;
using EmailService.Application.Registrations;
using EmailService.Persistence.Registrations;
using EmailService.WORKER;
using Mambo.Email.Concretes;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"))
        .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(path: $"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
    .Build();
var appSettings = new AppSettings();
configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

builder.Services.AddApplicationRegistrations(appSettings.ConsumerMassTransitSettings, appSettings.EmailSettings);
builder.Services.AddPersistenceRegistrations(appSettings.MongoDbSettings);

var host = builder.Build();
host.Run();