using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Data;
using CommandsService.Repositories;
using CommandsService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.DI
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
            services.AddScoped<ICommandRepo, CommandRepo>();

            return services;
        }
    }
}
