namespace Farfetch.Buildionaire.Data.Gateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class TfsCodeReviewGateway : ICodeReviewGateway
    {
        private TfsTeamProjectCollection tfsTeamProjectCollection;

        public TfsCodeReviewGateway(TfsConnection tfsConnection)
        {
            this.tfsTeamProjectCollection = tfsConnection.Connect();
        }

        private WorkItemStore WorkItemStore
        {
            get
            {
                return this.tfsTeamProjectCollection.GetService<WorkItemStore>();
            }
        }

        public IEnumerable<CodeReview> GetCodeReviews(DateTime sinceDate)
        {
            return this.GetCodeReviewRequests(sinceDate).Union(this.GetCodeReviewResponses(sinceDate));
        }

        private IEnumerable<CodeReview> GetCodeReviewRequests(DateTime since)
        {
            var discussionService = this.WorkItemStore;

            var queryRequest =
                discussionService.Query(
                    "Select * From WorkItems WHERE [System.WorkItemType] = 'Code Review Request'")
                    .Query.RunQuery()
                    .OfType<WorkItem>()
                    .Where(wi => wi.CreatedDate > since)
                    .AsEnumerable();
            return
                queryRequest.Select(
                    x => new CodeReview
                    {
                        CreatedAt = x.CreatedDate,
                        RequestedBy = new User { Name = x.CreatedBy },
                        Type = CodeReviewType.Request,
                        ExternalId = x.Id
                    });
        }

        private IEnumerable<CodeReview> GetCodeReviewResponses(DateTime since)
        {
            var discussionService = this.WorkItemStore;

            var queryResponse =
                discussionService.Query("Select * From WorkItems WHERE [System.WorkItemType] = 'Code Review Response'")
                    .Query.RunQuery()
                    .OfType<WorkItem>()
                    .Where(wi => wi.CreatedDate > since && wi.Fields.Contains("Reviewed By"))
                    .AsEnumerable();

            return
                queryResponse.Select(
                    x =>
                    new CodeReview
                        {
                            CreatedAt = x.CreatedDate,
                            Type = CodeReviewType.Response,
                            ExternalId = x.Id,
                            RequestedBy = new User { Name = x.Fields["Reviewed By"].Value.ToString() }
                        });
        }
    }
}
