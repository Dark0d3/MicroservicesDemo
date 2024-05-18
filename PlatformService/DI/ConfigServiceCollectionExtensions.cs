using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Repositories;
using PlatformService.Repositories.Interfaces;
using PlatformService.SyncDataServices.Http;
using PlatformService.SyncDataServices.Http.Interfaces;

namespace PlatformService.DI
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyGroup(
            this IServiceCollection services,
            IWebHostEnvironment env,
            IConfiguration configuration
        )
        {
            if (env.IsProduction())
            {
                Console.WriteLine("Using mssql");
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("platformsConn"))
                );
            }
            else
            {
                Console.WriteLine("Using InMem");
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            }

            services.AddScoped<IPlatformRepo, PlatformRepo>();
            services.AddHttpClient<ICommandDataClient, CommandDataClient>();

            return services;
        }
    }
}
