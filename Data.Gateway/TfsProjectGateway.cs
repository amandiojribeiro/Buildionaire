namespace Farfetch.Buildionaire.Data.Gateway
{
    using System.Collections.Generic;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;

    using Project = Farfetch.Buildionaire.Domain.Model.Project;

    public class TfsProjectGateway : IProjectGateway
    {
        private TfsTeamProjectCollection tfsTeamProjectCollection;

        public TfsProjectGateway(TfsConnection tfsConnection)
        {
            this.tfsTeamProjectCollection = tfsConnection.Connect();
        }

        private VersionControlServer VersionControlServer
        {
            get
            {
                return this.tfsTeamProjectCollection.GetService<VersionControlServer>();
            }
        }

        public IEnumerable<Project> GetProjects()
        {
            var teamProjects = this.VersionControlServer.GetAllTeamProjects(true);

            return teamProjects != null ? teamProjects.Select(x => new Project { Name = x.Name }) : new List<Project>();
        }
    }
}
