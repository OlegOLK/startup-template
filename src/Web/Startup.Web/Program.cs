using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Prometheus.DotNetRuntime;
using Serilog;
using System;

namespace Startup.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            _ = DotNetRuntimeStatsBuilder
                            .Customize()
                            .WithContentionStats()
                            .WithJitStats()
                            .WithThreadPoolStats()
                            .WithGcStats()
                            .WithExceptionStats()
                            .StartCollecting();

            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, builder) =>
            {
                Log.Logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(context.Configuration)
                                .CreateLogger();
            })
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}
