using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isPord = false)
        {
            using (var sevicesScope = app.ApplicationServices.CreateScope())
            {
                SeedData(sevicesScope.ServiceProvider.GetService<AppDbContext>(), isPord);
            }
        }

        private static void SeedData(AppDbContext context, bool isPord = false)
        {
            if (isPord)
            {
                try
                {
                    Console.WriteLine("Data migration");
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed Data migration {ex}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("Seeding data");
                context.Platforms.AddRange(
                    new Platform()
                    {
                        Name = "Test",
                        Publisher = "test 1",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "Test2",
                        Publisher = "test 2",
                        Cost = "Free2"
                    },
                    new Platform()
                    {
                        Name = "Test2",
                        Publisher = "test 2",
                        Cost = "Free2"
                    }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data");
            }
        }
    }
}
