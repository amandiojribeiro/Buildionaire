namespace Farfetch.Buildionaire.Application.Services.Management
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters;

    public class BuildServices : IBuildServices
    {
        private readonly IBuildRepository buildRepository;

        public BuildServices(IBuildRepository buildRepository)
        {
            this.buildRepository = buildRepository;
        }

        public BuildDto GetBuild(string buildName)
        {
            var result = this.buildRepository.Find(x => x.Name.Equals(buildName));
            return TypeAdapterHelper.Adapt<BuildDto>(result);
        }

        public IEnumerable<BuildDto> GetBuilds()
        {
            var result = this.buildRepository.GetAll();
            return TypeAdapterHelper.Adapt<List<BuildDto>>(result);
        }
    }
}
