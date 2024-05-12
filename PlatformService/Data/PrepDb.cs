using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var sevicesScope = app.ApplicationServices.CreateScope())
            {
                SeedData(sevicesScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
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
