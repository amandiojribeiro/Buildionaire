namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Application.Dto;

    public interface IScoreDetailsService
    {
        List<BuildScoreDetailDto> CalulateScoreDetails(BuildDto build);
    }
}
