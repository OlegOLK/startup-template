using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Startup.Web.Cors
{
    public static class DefaultCorsPolicy
    {
        public const string DefaultPolicyName = "localhost";
        public const string PolicySectionName = "Cors";
        public static void CreateDefaultPolicy(this CorsOptions options, IConfiguration configuration)
        {
            options.AddPolicy(DefaultPolicyName, builder =>
            {
                builder
                    .WithOrigins(configuration.GetSection(PolicySectionName).Get<CorsSettings>().AllowedOrigins)
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
            });
        }
    }
}
