using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using VH.Core.Configuration;

namespace InstantMessagingAPI
{
    public class Program
    {
        protected Program()
        {
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            const string vhInfraCore = "/mnt/secrets/vh-infra-core";
            const string vhInstantMessagingApi = "/mnt/secrets/vh-instantmessaging-api";

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((configBuilder) =>
                {
                    configBuilder.AddAksKeyVaultSecretProvider(vhInfraCore);
                    configBuilder.AddAksKeyVaultSecretProvider(vhInstantMessagingApi);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
