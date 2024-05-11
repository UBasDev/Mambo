using CoreService.API;
using CoreService.API.Registrations;
using CoreService.Application.Models;
using CoreService.Application.Registrations;
using CoreService.Persistence.Registrations;
using Elastic.Apm.NetCoreAll;
using Elastic.Apm.SerilogEnricher;
using Mambo.APM;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";
var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"))
    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(path: $"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
.Build();

#region AppSettings

var appSettings = new AppSettings();
configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

#endregion AppSettings

// Add services to the container.

#region Serilog

SetCustomLogger(configuration, environment, appSettings.ElasticsearchSettings.ElasticsearchUrl);

#endregion Serilog

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationRegistrations(appSettings.MamboCoreDbConnectionString);
builder.Services.AddPresentationRegistrations(appSettings.JwtSettings, appSettings.CorsOptions);
builder.Services.AddPersistenceRegistrations();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseAllElasticApm(configuration);
app.UseSerilogRequestLogging(requestLogOptions =>
{
    requestLogOptions.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("Environment", environment);
    };
});

#region Prometheus

app.UseOpenTelemetryApm();

#endregion Prometheus

#region healthcheck

app.MapGet("healthcheck", () => Results.Ok()).ShortCircuit();

#endregion healthcheck

app.UsePresentationRegistrations();

// Configure the HTTP request pipeline.
if (environment != "Production")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler(_ => { });
app.Run();

#region Serilog

static void SetCustomLogger(IConfigurationRoot configuration, string environment, string elasticsearchUrl)
{
    Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.WithProperty("Environment", environment)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .Enrich.WithElasticApmCorrelationInfo()
                .WriteTo.Elasticsearch(ConfigureElasticSink(environment, elasticsearchUrl))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
}
static ElasticsearchSinkOptions ConfigureElasticSink(string environment, string elasticsearchUrl)
{
    return new ElasticsearchSinkOptions(new Uri(elasticsearchUrl))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"mambo-core-service-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
        NumberOfReplicas = 1,
        NumberOfShards = 2,
        ConnectionTimeout = TimeSpan.FromSeconds(10)
    };
}

#endregion Serilog

public partial class Program;