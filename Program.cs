using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TravelTracker.Models;

namespace TravelTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InsertData();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InsertData()
        {
            using (var context = new TrekContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                // Adds an Owner
                //var owner = new Owner
                //{
                //    Id = Guid.Parse("3bf33e35-77e4-4110-89b3-dc6a4b97ed43"),
                //    Name = "Nigel"
                //};
                //context.Owners.Add(owner);

                //// Adds some Treks
                //context.Trek.Add(new Trek
                //{
                //    Id = Guid.NewGuid(),
                //    Area = "North West Lakes",
                //    Duration = 213,
                //    Weather = Weather.Bright,
                //    TrekDate = DateTime.Now.ToUniversalTime().,
                //    Summary = "Lovely walk with friends and family. Stopped at the pub for lunch",
                //    Owner = owner
                //});
                //context.Trek.Add(new Trek
                //{
                //    Id = Guid.NewGuid(),
                //    Area = "South West Lakes",
                //    Duration = 321,
                //    Weather = Weather.HeavyRain,
                //    TrekDate = DateTime.Now,
                //    Summary = "Grimmiced to the end!!",
                //    Owner = owner
                //});

                // Saves changes
                context.SaveChanges();
            }
        }
    }
}
