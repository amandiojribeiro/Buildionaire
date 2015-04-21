namespace Farfetch.Buildionaire.Domain.Core.GatewayInterfaces
{
    using System;
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Model;

    public interface IChangesetGateway
    {
        IEnumerable<ChangeSet> GetChangesets(DateTime sinceDate);

        IEnumerable<ChangeSet> GetChangesets(DateTime sinceDate, string projectPath);
    }
}
