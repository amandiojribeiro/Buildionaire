namespace Farfetch.Buildionaire.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;

    public class ScoreService : IScoreService
    {
        private readonly ScoreStrategyFactory scoreStrategyFactory;

        public ScoreService(ScoreStrategyFactory scoreStrategyFactory)
        {
            this.scoreStrategyFactory = scoreStrategyFactory;
        }

        public BuildScoreDetail CalculateScoreDetail(Build build, Metric metric)
        {
            return new BuildScoreDetail
                                  {
                                      Score =
                                          this.scoreStrategyFactory.GetScoreStrategy(metric.Code)
                                          .GetScore(build, metric),
                                      Build = build,
                                      Metric = metric
                                  };
        }

        public IEnumerable<BuildScoreDetail> CalculateScoreDetail(Build build, IEnumerable<Metric> metric)
        {
            return metric.Select(x => this.CalculateScoreDetail(build, x));
        }
    }
}