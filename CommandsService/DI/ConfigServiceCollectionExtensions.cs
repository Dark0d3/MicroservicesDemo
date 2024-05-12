using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.DI
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            // services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            // services.AddScoped<IPlatformRepo, PlatformRepo>();

            return services;
        }
    }
}
