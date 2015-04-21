using Hangfire;

using System.Web.Http;

namespace Farfetch.Buildionaire.Presentation.Web.Controllers.Api
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Application.Services.DataImporter;
    using Farfetch.Buildionaire.Application.Services.Management;

    [RoutePrefix("Buildionaire/v1/Builds")]
    public class BuildsController : ApiController
    {

        private readonly IBuildServices manageBuildServices;
        private readonly IImportBuildServices importBuildServices;

        public BuildsController(IBuildServices manageBuildServices, IImportBuildServices importBuildServices)
        {
            this.manageBuildServices = manageBuildServices;
            this.importBuildServices = importBuildServices;
        }

        public IEnumerable<BuildDto> Get()
        {
            return this.manageBuildServices.GetBuilds();
        }

        [HttpPost]
        [Route("ResetRequest")]
        public void ResetDatabaseRequest()
        {
        }

        [HttpPost]
        [Route("ImportRequest")]
        public void ImportDataRequest()
        {
            this.importBuildServices.ImportBuilds();
        }

        [HttpPost]
        [Route("Metrics/RecalculateRequest")]
        public void RecalculateMetricsRequest()
        {

        }

        [HttpPost]
        [Route("ImportRequest/RecurringJob")]
        public void AddImportBuildsTask(string cronstring = "0 * * * *")
        {
            RecurringJob.AddOrUpdate<IImportBuildServices>("ImportBuilds", s => s.ImportBuilds(), cronstring);
        }

        [HttpDelete]
        [Route("ImportRequest/RecurringJob")]
        public void DeteleImportBuildsTask()
        {
            RecurringJob.RemoveIfExists("ImportBuilds");
        }
    }
}