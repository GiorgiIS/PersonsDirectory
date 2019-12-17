using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using TBCPersonsDirectory.Repository.EF;

namespace TBCPersonsDirectory.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.File(
               new Serilog.Formatting.Json.JsonFormatter(),
               @"Files/logs/log-.log",
               fileSizeLimitBytes: 1_000_000,
               rollOnFileSizeLimit: true,
               rollingInterval: RollingInterval.Day,
               shared: true,
               flushToDiskInterval: TimeSpan.FromSeconds(1))
               .CreateLogger();
            try
            {
                Log.Information("Starting web host");

                CreateHostBuilder(args)
                .Build()
                .MigrateDatabase()
                .Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("https://localhost:2727");
                });
    }
}
