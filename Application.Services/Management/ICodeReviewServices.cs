namespace Farfetch.Buildionaire.Application.Services.Management
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Model;

    public interface ICodeReviewServices
    {
        IEnumerable<CodeReview> GetCodeReviews();

        CodeReview GetCodeReviewById(int id);
    }
}
