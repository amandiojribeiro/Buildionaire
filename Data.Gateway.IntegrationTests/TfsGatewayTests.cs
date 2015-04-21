using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.Gateway.IntegrationTests
{
    using System.Linq;

    using Farfetch.Buildionaire.Data.Gateway;

    [TestClass]
    public class TfsGatewayTests
    {
        [TestMethod]
        public void GetProjects()
        {
            TfsBuildGateway target = new TfsBuildGateway();

            var projects = target.GetProjects();
        }

        [TestMethod]
        public void GetBuils()
        {
            TfsBuildGateway target = new TfsBuildGateway();

            var projects = target.GetBuilds(DateTime.Now.AddDays(-3), "Portal");
        }

        [TestMethod]
        public void GetChangeSets()
        {
            TfsBuildGateway target = new TfsBuildGateway();

            var codeReviews = target.GetChangesets(new DateTime(2015, 1, 1)).ToList();
        }
    }
}
