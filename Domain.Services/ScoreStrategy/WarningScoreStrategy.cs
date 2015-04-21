namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy
{
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Model;

    public class WarningScoreStrategy : IScoreStrategy
    {
        public int GetScore(Build build, Metric metric)
        {
            if (build.Warnings != null)
            {
                return metric.Weight * build.Warnings.Sum(x => x.Total);
            }

            return 0;
        }
    }
}
