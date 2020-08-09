using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApp.Models;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args)
            //            .UseKestrel()
            //            .UseIISIntegration()
            //            .Build().Run();

            var host = CreateWebHostBuilder(args)
                .UseKestrel()
                .UseIISIntegration()
                .Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<StreamDBContext>();
                if (!db.Database.EnsureCreated())
                    db.Database.Migrate();
            }
            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                   WebHost.CreateDefaultBuilder(args)
                       .UseStartup<Startup>();


    }
}
