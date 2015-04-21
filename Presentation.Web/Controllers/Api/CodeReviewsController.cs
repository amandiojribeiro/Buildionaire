using Farfetch.Buildionaire.Application.Services;

using Hangfire;

using System.Web.Http;

namespace Farfetch.Buildionaire.Presentation.Web.Controllers.Api
{
    using Farfetch.Buildionaire.Application.Services.DataImporter;
    using Farfetch.Buildionaire.Application.Services.Management;

    [RoutePrefix("Buildionaire/v1/Codereviews")]
    public class CodereviewsController : ApiController
    {

        private readonly IImportCodeReviewServices codeReviewServices;

        public CodereviewsController(IImportCodeReviewServices codeReviewServices)
        {
            this.codeReviewServices = codeReviewServices;
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
            this.codeReviewServices.ImportCodeReviews();
        }

        [HttpPost]
        [Route("ImportRequest/RecurringJob")]
        public void AddImportCodereviewsTask(string cronstring = "0 * * * *")
        {
            RecurringJob.AddOrUpdate<IImportCodeReviewServices>("ImportCodereviews", s => s.ImportCodeReviews(), cronstring);
        }

        [HttpDelete]
        [Route("ImportRequest/RecurringJob")]
        public void DeteleImportCodereviewsTask()
        {
            RecurringJob.RemoveIfExists("ImportCodereviews");
        }
    }
}