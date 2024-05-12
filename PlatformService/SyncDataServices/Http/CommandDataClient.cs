using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlatformService.Dtos;
using PlatformService.SyncDataServices.Http.Interfaces;

namespace PlatformService.SyncDataServices.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto readDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(readDto),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync(
                $"{_configuration["CommandService"]}",
                httpContent
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT OK");
            }
        }
    }
}
