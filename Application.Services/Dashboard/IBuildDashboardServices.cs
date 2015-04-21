namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Application.Dto;

    public interface IBuildDashboardServices
    {
        Task<IEnumerable<RankingBuildItemDto>> GetTopBuildsAsync();

        Task<IEnumerable<RankingBuildItemDto>> GetTopBuildsAsync(string dashboardCode);

        Task<IEnumerable<RankingBuildItemDto>> GetBottomBuildsAsync();

        Task<IEnumerable<RankingBuildItemDto>> GetBottomBuildsAsync(string dashboardCode);

        Task<IEnumerable<RankingBuildItemDto>> GetMonthTopBuildsAsync();

        Task<IEnumerable<RankingBuildItemDto>> GetMonthTopBuildsAsync(string dashboardCode);

        Task<IEnumerable<RankingBuildItemDto>> GetMonthBottomBuildsAsync();

        Task<IEnumerable<RankingBuildItemDto>> GetMonthBottomBuildsAsync(string dashboardCode);

        Task<IEnumerable<Tuple<string, decimal>>> GetBuildHourDistributionAsync();

        Task<IEnumerable<Tuple<string, decimal>>> GetMonthlyScoreAsync();

        Task<RankingBuildItemDto> GetLastFailedBuildAsync();

        Task<IEnumerable<Tuple<string, decimal>>> GetMonthlyScoreAsync(string dashboardCode);

        Task<int> GetTotalBuildsAsync();
    }
}
