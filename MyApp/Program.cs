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
using Microsoft.Extensions.Logging;
using MyApp.Models;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args)
                .UseKestrel()
                .UseIISIntegration()
                .Build();
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of PersonDBContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StreamDBContext>();
                //4. Call the DataGenerator to create sample data
                //SeedData.Initialize(services);

                if (!context.Database.EnsureCreated())
                    context.Database.Migrate();

            }
            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                   WebHost.CreateDefaultBuilder(args)
                       .UseStartup<Startup>();


    }
}
