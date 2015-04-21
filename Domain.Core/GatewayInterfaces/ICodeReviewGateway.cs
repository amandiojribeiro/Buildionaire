namespace Farfetch.Buildionaire.Domain.Core.GatewayInterfaces
{
    using System;
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Model;

    public interface ICodeReviewGateway
    {
        IEnumerable<CodeReview> GetCodeReviews(DateTime sinceDate);
    }
}
