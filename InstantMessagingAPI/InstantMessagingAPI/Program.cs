using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace InstantMessagingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        //const string vhInfraCore = "/mnt/secrets/vh-infra-core";
        //const string vhNotificationApi = "/mnt/secrets/vh-instant-messaging-api";

        //return Host.CreateDefaultBuilder(args)
        //    .ConfigureAppConfiguration((configBuilder) =>
        //    {
        //        configBuilder.AddAksKeyVaultSecretProvider(vhInfraCore);
        //        configBuilder.AddAksKeyVaultSecretProvider(vhNotificationApi);
        //    })
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
        //        webBuilder.UseIISIntegration();
        //        webBuilder.UseStartup<Startup>();
        //    });
    }
}