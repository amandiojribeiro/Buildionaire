namespace Farfetch.Buildionaire.Presentation.Web.Controllers
{
    using System.Web.Mvc;

    using Farfetch.Buildionaire.Application.Services.DataImporter;

    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index(string id)
        {
            return this.View((object)(string.IsNullOrEmpty(id) ? "''" : "'" + id + "'"));
        }
    }
}