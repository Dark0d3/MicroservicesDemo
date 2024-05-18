using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.EventProcessing.Interfaces;
using CommandsService.Models;
using CommandsService.Repositories.Interfaces;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scope;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _scope = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string eventString)
        {
            var eventType = DetermineEvent(eventString);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(eventString);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining  Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> could not determine the  event type");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _scope.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(
                    platformPublishedMessage
                );

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repo.ExternalPlatformsExist(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> Platform created");
                    }
                    else
                    {
                        Console.WriteLine("--> Platform already exist");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> could not add Platform to DB {ex.Message}");
                }
            }
        }

        enum EventType
        {
            PlatformPublished,
            Undetermined
        }
    }
}
