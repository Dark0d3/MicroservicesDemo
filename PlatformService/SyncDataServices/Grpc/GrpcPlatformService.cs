using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using PlatformService.Repositories.Interfaces;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(
            GetAllRequest request,
            ServerCallContext context
        )
        {
            var response = new PlatformResponse();
            var platforms = _repository.GetAllPlatforms();

            foreach (var platform in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
            }

            return Task.FromResult(response);
        }
    }
}
