using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController() { }

        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound at Command");
            return Ok("Inbound test from PlatformsController");
        }
    }
}
