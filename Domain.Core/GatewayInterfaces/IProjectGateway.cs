namespace Farfetch.Buildionaire.Domain.Core.GatewayInterfaces
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Model;

    public interface IProjectGateway
    {
        IEnumerable<Project> GetProjects();
    }
}
