using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder => { builder.UseStartup<Startup>(); })
                .UseSerilog((host, log) => { log
                    .ReadFrom.Configuration(host.Configuration)
                    .Enrich.FromLogContext();
                });
        }
    }
}