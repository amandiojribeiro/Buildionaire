namespace Farfetch.Buildionaire.Domain.Core.GatewayInterfaces
{
    using System;
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Model;

    public interface IBuildGateway
    {
        IEnumerable<Build> GetBuilds(DateTime sinceDate);

        IEnumerable<Build> GetBuilds(DateTime sinceDate, string project);
    }
}
