namespace Farfetch.Buildionaire.Application.Services.Management
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Application.Dto;

    public interface IBuildServices
    {
        BuildDto GetBuild(string buildName);

        IEnumerable<BuildDto> GetBuilds();
    }
}
