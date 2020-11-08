using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory()).Build();
            host.Run();
            // var host = new WebHostBuilder()
            //.UseKestrel()
            //.UseContentRoot(Directory.GetCurrentDirectory())
            //.UseIISIntegration()
            //.UseStartup<Startup>()
            //.Build();

            // host.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>()
        //                .UseUrls("http://localhost:4000");
        //        });
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.ConfigureKestrel(serverOptions =>
    {
        serverOptions.Limits.MaxConcurrentConnections = 100;
        serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
        serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
        serverOptions.Limits.MinRequestBodyDataRate =
            new MinDataRate(bytesPerSecond: 100,
                gracePeriod: TimeSpan.FromSeconds(10));
        serverOptions.Limits.MinResponseDataRate =
            new MinDataRate(bytesPerSecond: 100,
                gracePeriod: TimeSpan.FromSeconds(10));
        serverOptions.Limits.KeepAliveTimeout =
            TimeSpan.FromMinutes(2);
        serverOptions.Limits.RequestHeadersTimeout =
            TimeSpan.FromMinutes(1);
    })
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration()
    .UseStartup<Startup>();
});
    }
}