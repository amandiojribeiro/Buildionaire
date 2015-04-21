namespace Farfetch.Buildionaire.Application.Services.Management
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    public class CodeReviewServices : ICodeReviewServices
    {
        private readonly ICodeReviewRepository codeReviewRepository;

        public CodeReviewServices(ICodeReviewRepository codeReviewRepository)
        {
            this.codeReviewRepository = codeReviewRepository;
        }

        public IEnumerable<CodeReview> GetCodeReviews()
        {
            return this.codeReviewRepository.GetAll();
        }

        public CodeReview GetCodeReviewById(int id)
        {
            return this.codeReviewRepository.Get(id);
        }
    }
}
