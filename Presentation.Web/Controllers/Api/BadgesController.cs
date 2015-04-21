namespace Farfetch.Buildionaire.Presentation.Web.Controllers.Api
{
    using System.Web.Http;

    using Farfetch.Buildionaire.Application.Services.DataImporter;

    using Hangfire;

    [RoutePrefix("Buildionaire/v1/Badges")]
    public class BadgesController : ApiController
    {
        private readonly IBadgesService badgesServices;

        public BadgesController(IBadgesService badgesServices)
        {
            this.badgesServices = badgesServices;
        }

        [HttpPost]
        [Route("ResetRequest")]
        public void ResetDatabaseRequest()
        {

        }

        [HttpPost]
        [Route("CalculateRequest")]
        public void CalculateRequest()
        {
            this.badgesServices.CalculateBadges();
        }

        [HttpPost]
        [Route("CalculateRequest/RecurringJob")]
        public void AddImportBadgesTask(string cronstring = "0 * * * *")
        {
            RecurringJob.AddOrUpdate<IBadgesService>("ImportBadges", s => s.CalculateBadges(), cronstring);
        }

        [HttpDelete]
        [Route("CalculateRequest/RecurringJob")]
        public void DeteleImportBadgesTask()
        {
            RecurringJob.RemoveIfExists("ImportBadges");
        }


    }
}