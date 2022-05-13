using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using worksheet2.Data;

namespace worksheet2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            
            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            var serviceProvider = host.Services;
            using var scope = serviceProvider.CreateScope();
            var provider = scope.ServiceProvider;
            try
            {
                var context = provider.GetRequiredService<BankContext>();
                DatabaseInitializer.CreateIfNotCreatedAlready(context);
            }
            catch (Exception ex)
            {
                var logger = provider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "DB initialization failed");
            }
        }
    }
}