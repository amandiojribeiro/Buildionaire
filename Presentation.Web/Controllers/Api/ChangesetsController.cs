namespace Farfetch.Buildionaire.Presentation.Web.Controllers.Api
{
    using System.Web.Http;

    using Farfetch.Buildionaire.Application.Services;
    using Farfetch.Buildionaire.Application.Services.DataImporter;

    using Hangfire;

    [RoutePrefix("Buildionaire/v1/Changesets")]
    public class ChangesetsController : ApiController
    {

        private readonly IImportChangesetServices changesetServices;

        public ChangesetsController(IImportChangesetServices changesetServices)
        {
            this.changesetServices = changesetServices;
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
            this.changesetServices.ImportChangesets();
        }


        [HttpPost]
        [Route("ImportRequest/RecurringJob")]
        public void AddImportChangesetsTask(string cronstring = "0 * * * *")
        {
            RecurringJob.AddOrUpdate<IImportChangesetServices>("ImportChangesets", s => s.ImportChangesets(), cronstring);
        }

        [HttpDelete]
        [Route("ImportRequest/RecurringJob")]
        public void DeteleImportChangesetsTask()
        {
            RecurringJob.RemoveIfExists("ImportChangesets");
        }


    }
}