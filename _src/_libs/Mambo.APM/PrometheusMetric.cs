using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mambo.APM
{
    public static class PrometheusMetric
    {
        public static void AddPrometheusMetrics(this IServiceCollection services)
        {
        }

        public static void UsePrometheusMetrics(this IApplicationBuilder app)
        {
            app.UseMetricServer(configure =>
            {
            }, url: "/metrics");
            app.UseHttpMetrics(options =>
            {
                options.AddCustomLabel("host", context => context.Request.Host.Host);
            });
        }
    }
}