namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using System;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    public class ImportCodeReviewServices : IImportCodeReviewServices
    {
        private const string Startdate = "01/01/2014";

        private readonly ICodeReviewRepository codeReviewRepository;

        private readonly IUserRepository userRepository;

        private readonly ICodeReviewGateway codeReviewGateway;

        public ImportCodeReviewServices(ICodeReviewRepository codeReviewRepository, IUserRepository userRepository, ICodeReviewGateway codeReviewGateway)
        {
            this.codeReviewRepository = codeReviewRepository;
            this.userRepository = userRepository;
            this.codeReviewGateway = codeReviewGateway;
        }

        public void ImportCodeReviews()
        {
            var lastCodeReview = this.codeReviewRepository.GetLast();

            var beginingDate = lastCodeReview != null ? lastCodeReview.CreatedAt : DateTime.Parse(Startdate);

            var codeReviews = this.codeReviewGateway.GetCodeReviews(beginingDate);

            foreach (var codeReview in codeReviews)
            {
                var externalId = codeReview.ExternalId;
                if (this.codeReviewRepository.Exists(e => e.ExternalId == externalId))
                {
                    continue;
                }

                var requestedBy = codeReview.RequestedBy.Name;
                var user = this.userRepository.Find(x => x.Name.Equals(requestedBy));

                if (user != null)
                {
                    codeReview.RequestedBy = user;
                }

                this.codeReviewRepository.Add(codeReview);
            }
        }
    }
}
