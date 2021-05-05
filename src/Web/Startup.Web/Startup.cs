using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using Prometheus.SystemMetrics;
using Startup.Web.Cors;
using Startup.Web.Middlewares;
using Startup.Web.Swagger;
using Prometheus.HttpClientMetrics;

namespace Startup.Web
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHealthChecks();
            services.AddSystemMetrics();
            services.AddDataProtection(s =>
            {
                s.ApplicationDiscriminator = "Startup.Web";
            });

            services.AddCors(s =>
            {
                s.CreateDefaultPolicy(Configuration);
            });

            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger(provider);
            }
            app.UseCustomMiddlewares(env);
            app.UseRouting();
            app.UseHttpMetrics();
            
            app.UseCors(DefaultCorsPolicy.DefaultPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapMetrics();
            });
        }
    }
}
