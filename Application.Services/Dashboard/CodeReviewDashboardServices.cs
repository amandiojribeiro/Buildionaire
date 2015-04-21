namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters;

    public class CodeReviewDashboardServices : ICodeReviewDashboardServices
    {
        private readonly IUserRepository userRepository;

        public CodeReviewDashboardServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<RankingReviewerItemDto>> GetMonthTopCodeReviewersAsync()
        {
            var currentDate = DateTime.Now.AddMonths(-1);
            var topRankingCodeReview = await this.userRepository.GetMonthTopRankingReviewersAsync(currentDate.Year, currentDate.Month, 3);

            return topRankingCodeReview.Select(x => new RankingReviewerItemDto { Id = x.Item1.Id, Name = x.Item1.Name, TotalReviews = x.Item2 });
        }

        public async Task<IEnumerable<RankingReviewerItemDto>> GetTopCodeReviewersAsync()
        {
            var topRankingCodeReview = await this.userRepository.GetTopRankingReviewersAsync(3);

            return TypeAdapterHelper.Adapt<List<RankingReviewerItemDto>>(topRankingCodeReview);
        }
    }
}
