namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Core;

    public class ScoreDashboardServices : IScoreDashboardServices
    {
        private readonly IBuildScoreDetailRepository buildScoreDetailRepository;

        public ScoreDashboardServices(IBuildScoreDetailRepository buildScoreDetailRepository)
        {
            this.buildScoreDetailRepository = buildScoreDetailRepository;
        }

        public async Task<int> GetTotalBuildCoinsAsync()
        {
            return await Task.FromResult(this.buildScoreDetailRepository.Sum(x => x.Score));
        }
    }
}
