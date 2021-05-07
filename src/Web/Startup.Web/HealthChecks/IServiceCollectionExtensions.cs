using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;

namespace Startup.Web.HealthChecks
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWebHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck("dummy", () =>
            {
                return new HealthCheckResult(HealthStatus.Healthy);
            }).ForwardToPrometheus();
            services.AddHealthChecksUI().AddInMemoryStorage();
            return services;
        }
    }
}
