using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;

namespace CommandsService.SyncDataServices.Grpc.Interfaces
{
    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}
