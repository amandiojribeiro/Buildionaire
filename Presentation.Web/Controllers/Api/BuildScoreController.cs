namespace Farfetch.Buildionaire.Presentation.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Application.Services.DataImporter;

    public class BuildScoreController : ApiController
    {
        private IScoreDetailsService scoreService;

        public BuildScoreController(IScoreDetailsService scoreService)
        {
            this.scoreService = scoreService;
        }

        public List<BuildScoreDetailDto> Post(BuildDto build)
        {
            return this.scoreService.CalulateScoreDetails(build);
        }
    }
}
