using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.AsyncDataServices;
using CommandsService.Data;
using CommandsService.EventProcessing;
using CommandsService.EventProcessing.Interfaces;
using CommandsService.Repositories;
using CommandsService.Repositories.Interfaces;
using CommandsService.SyncDataServices.Grpc;
using CommandsService.SyncDataServices.Grpc.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.DI
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
            services.AddScoped<ICommandRepo, CommandRepo>();
            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddHostedService<MessageBusSubscriber>();
            services.AddScoped<IPlatformDataClient, PlatformDataClient>();

            return services;
        }
    }
}
