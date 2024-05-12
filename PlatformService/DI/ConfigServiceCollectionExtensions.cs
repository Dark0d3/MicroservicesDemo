using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Repositories;
using PlatformService.Repositories.Interfaces;

namespace PlatformService.DI
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            services.AddScoped<IPlatformRepo, PlatformRepo>();

            return services;
        }
    }
}
