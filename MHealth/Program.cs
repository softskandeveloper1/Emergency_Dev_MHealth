using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MHealth
{
    public class Program
    {
        //public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //     .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        //     .Build();


        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //     .ReadFrom.Configuration(Configuration)
            //     .CreateLogger();

       


            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.Seq("http://localhost:5341")
           .CreateLogger();


            try
            {
                Log.Information("Getting the motors running...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
                {

                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .CreateLogger();
                }

                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                //            .UseSerilog((hostingContext, loggerConfiguration) => {
                //                loggerConfiguration
                //                    .ReadFrom.Configuration(hostingContext.Configuration)
                //                    .Enrich.FromLogContext()
                //                    .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                //                    .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);

                //#if DEBUG
                //                // Used to filter out potentially bad data due debugging.
                //                // Very useful when doing Seq dashboards and want to remove logs under debugging session.
                //                loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
                //#endif
                //            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Listen(IPAddress.Loopback, 5001, listenOptions =>
                        {
                            listenOptions.UseHttps();
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
