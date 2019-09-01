using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cosmosdb.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logbuilder) =>
                {
                    logbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logbuilder.AddConsole();
                })
                .ConfigureAppConfiguration((builderContext, config) =>
                    {
                        IHostingEnvironment env =
                            builderContext.HostingEnvironment;
                        config
                            .AddJsonFile("appsettings.json", optional:
                                false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    })
                .UseStartup<Startup>();

    }
}
