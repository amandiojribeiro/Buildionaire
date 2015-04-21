namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy
{
    using System;

    using Farfetch.Buildionaire.Domain.Model;

    public class CodeCoverageScoreStrategy : IScoreStrategy
    {
        public int GetScore(Build build, Metric metric)
        {
            if(build.CodeCoverage == null)
            {
                return 0;
            }

            if(build.CodeCoverage.TotalBlocks <= 0)
            {
                return 0;
            }

            var coverage =  build.CodeCoverage.CoveredBlocks * 100.0 / build.CodeCoverage.TotalBlocks;

            return (int)Math.Round(metric.Weight * coverage);
        }
    }
}
