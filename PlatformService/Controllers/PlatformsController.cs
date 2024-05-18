using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices.Interfaces;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Repositories.Interfaces;
using PlatformService.SyncDataServices.Http.Interfaces;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            IPlatformRepo repo,
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient
        )
        {
            _repository = repo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms");
            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformsById")]
        public ActionResult<PlatformReadDto> GetPlatformsById(int id)
        {
            Console.WriteLine("--> Getting Platform by id");
            var platform = _repository.GetPlatformById(id);
            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(
            PlatformCreateDto platformCreateDto
        )
        {
            Console.WriteLine("--> Creating Platform");
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var PlatformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            //send sync message
            try
            {
                await _commandDataClient.SendPlatformToCommand(PlatformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not send {ex.Message}");
            }

            //send async meesage
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(PlatformReadDto);
                platformPublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not send async {ex.Message}");
            }

            return CreatedAtRoute(
                nameof(GetPlatformsById),
                new { Id = PlatformReadDto.Id },
                PlatformReadDto
            );
        }
    }
}
