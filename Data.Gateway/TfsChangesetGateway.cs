namespace Farfetch.Buildionaire.Data.Gateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public class TfsChangesetGateway : IChangesetGateway
    {
        private readonly TfsTeamProjectCollection tfsTeamProjectCollection;

        public TfsChangesetGateway(TfsConnection connection)
        {
            this.tfsTeamProjectCollection = connection.Connect();
        }

        public IEnumerable<ChangeSet> GetChangesets(DateTime sinceDate)
        {
            return this.GetChangesets(sinceDate, "$/");
        }

        public IEnumerable<ChangeSet> GetChangesets(DateTime sinceDate, string projectPath)
        {
            var vcs = this.tfsTeamProjectCollection.GetService<VersionControlServer>();
            VersionSpec versionFrom = GetDateVSpec(sinceDate);
            VersionSpec versionTo = GetDateVSpec(sinceDate.AddDays(15));
            var changeSets = vcs.QueryHistory(projectPath, VersionSpec.Latest, 0, RecursionType.Full, null, versionFrom, versionTo, 1000, false, false).OfType<Changeset>().ToList();
            return changeSets.Select(x => new ChangeSet { User = new User { Name = x.OwnerDisplayName }, CreatedAt = x.CreationDate, ExternalChangesetId = x.ChangesetId });
        }

        private static VersionSpec GetDateVSpec(DateTime date)
        {
            string dateSpec = string.Format("D{0:yyy}-{0:MM}-{0:dd}T{0:HH}:{0:mm}", date);
            return VersionSpec.ParseSingleSpec(dateSpec, string.Empty);
        }
    }
}
