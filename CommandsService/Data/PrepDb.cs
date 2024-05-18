using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;
using CommandsService.Repositories.Interfaces;
using CommandsService.SyncDataServices.Grpc.Interfaces;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms = grpcClient.ReturnAllPlatforms();
                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
            }
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("Seeding Data");
            foreach (var platform in platforms)
            {
                if (!repo.ExternalPlatformsExist(platform.ExternalID))
                {
                    repo.CreatePlatform(platform);
                    repo.SaveChanges();
                }
                else
                {
                    Console.WriteLine("already in");
                }
            }
        }
    }
}
