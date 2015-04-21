namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters;

    public class BuildDashboardServices : IBuildDashboardServices
    {
        private readonly IBuildRepository buildRepository;

        private readonly IDashboardRepository dashboardRepository;

        public BuildDashboardServices(IBuildRepository buildRepository, IDashboardRepository dashboardRepository)
        {
            this.buildRepository = buildRepository;
            this.dashboardRepository = dashboardRepository;
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetTopBuildsAsync()
        {
            return await this.GetTopBuildsAsync(null);
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetTopBuildsAsync(string dashboardCode)
        {
            var top3Builds = await this.buildRepository.GetHighlitedBuilds(true, 3, dashboardCode);

            return top3Builds.Select(this.AdaptBuild);
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetBottomBuildsAsync()
        {
            return await this.GetBottomBuildsAsync(null);
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetBottomBuildsAsync(string dashboardCode)
        {
            var bottomBuilds = await this.buildRepository.GetHighlitedBuilds(false, 3, dashboardCode);

            return bottomBuilds.Select(this.AdaptBuild);
        }

        public async Task<IEnumerable<Tuple<string, decimal>>> GetBuildHourDistributionAsync()
        {
            var ret = await this.dashboardRepository.GetCheckinsPerHourAsync();

            return ret.Select(x => new Tuple<string, decimal>(x.Item1, x.Item2));
        }

        public async Task<RankingBuildItemDto> GetLastFailedBuildAsync()
        {
            var lastFailedBuild = await this.buildRepository.GetLastFailedBuildAsync();

            return TypeAdapterHelper.Adapt<RankingBuildItemDto>(lastFailedBuild);
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetMonthTopBuildsAsync()
        {
            return await this.GetMonthTopBuildsAsync(null);
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetMonthTopBuildsAsync(string dashboardCode)
        {
            var currentDate = DateTime.Now.AddMonths(-1);
            var bottomBuilds = await this.buildRepository.GetMonthHighlitedBuilds(currentDate.Year, currentDate.Month, true, 3, dashboardCode);

            return bottomBuilds.Select(this.AdaptBuild);
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetMonthBottomBuildsAsync()
        {
            return await this.GetMonthBottomBuildsAsync(null);
        }

        public async Task<IEnumerable<RankingBuildItemDto>> GetMonthBottomBuildsAsync(string dashboardCode)
        {
            var currentDate = DateTime.Now.AddMonths(-1);
            var bottomBuilds = await this.buildRepository.GetMonthHighlitedBuilds(currentDate.Year, currentDate.Month, false, 3, dashboardCode);

            return bottomBuilds.Select(this.AdaptBuild);
        }

        public async Task<IEnumerable<Tuple<string, decimal>>> GetMonthlyScoreAsync()
        {
            var ret = await this.dashboardRepository.GetMonthlyScoreAsync();

            return ret.Select(x => new Tuple<string, decimal>(x.Item1, x.Item2));
        }

        public async Task<IEnumerable<Tuple<string, decimal>>> GetMonthlyScoreAsync(string dashboardCode)
        {
            var ret = await this.dashboardRepository.GetMonthlyScoreAsync(dashboardCode);

            return ret.Select(x => new Tuple<string, decimal>(x.Item1, x.Item2));
        }

        public async Task<int> GetTotalBuildsAsync()
        {
            return await Task.FromResult(this.buildRepository.Count());
        }

        private RankingBuildItemDto AdaptBuild(Build build, int index)
        {
            return new RankingBuildItemDto
            {
                Id = build.Id,
                BuildDate = build.CreatedAt,
                Name = build.Name,
                Position = index,
                Score = build.TotalScore
            };
        }
    }
}
