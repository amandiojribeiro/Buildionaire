namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Application.Dto;

    public interface ICodeReviewDashboardServices
    {
        Task<IEnumerable<RankingReviewerItemDto>> GetTopCodeReviewersAsync();

        Task<IEnumerable<RankingReviewerItemDto>> GetMonthTopCodeReviewersAsync();
    }
}
