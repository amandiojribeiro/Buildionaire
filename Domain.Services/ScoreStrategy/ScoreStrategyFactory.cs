namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy
{
    using System;
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Model;

    public class ScoreStrategyFactory
    {
        private readonly Dictionary<string, IScoreStrategy> availableStrategies;

        public ScoreStrategyFactory()
        {
            this.availableStrategies = new Dictionary<string, IScoreStrategy>
                                           {
                                               { Metric.BuildStatus, new BuildStatusScoreStrategy() },
                                               { Metric.CodeCoverage, new CodeCoverageScoreStrategy() },
                                               { Metric.Error, new ErrorScoreStrategy() },
                                               { Metric.Warning, new WarningScoreStrategy() },
                                               { Metric.CodeReview, new CodeReviewScoreStrategy() }
                                           };
        }

        public virtual IScoreStrategy GetScoreStrategy(string metricCode)
        {
            if (!this.availableStrategies.ContainsKey(metricCode))
            {
                throw new ArgumentException("Metric Code not supported", "metricCode");
            }

            return this.availableStrategies[metricCode];
        }
    }
}
