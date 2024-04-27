using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Mambo.APM
{
    public static class OpenTelemetryApm
    {
        public static void AddOpenTelemetryApm(this IServiceCollection services)
        {
            /*SDK

            var metricProvider = Sdk.CreateMeterProviderBuilder()
             .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceName1"))
            .AddMeter("meterName1")
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddPrometheusExporter(name: "prometheus1", opt =>
            {
            })
            .Build();
            services.AddSingleton(metricProvider);
            SDK */
            services.AddOpenTelemetry().WithMetrics(metricOpt =>
            {
                metricOpt.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceName1")).AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel", "System.Net.Http").AddRuntimeInstrumentation().AddAspNetCoreInstrumentation().AddHttpClientInstrumentation().AddProcessInstrumentation().AddPrometheusExporter("prometheus1", opt =>
                {
                });
            });
            //.WithTracing()
        }

        public static void UseOpenTelemetryApm(this IApplicationBuilder app)
        {
            app.UseOpenTelemetryPrometheusScrapingEndpoint(context => context.Request.Path == "/metrics");
        }
    }
}