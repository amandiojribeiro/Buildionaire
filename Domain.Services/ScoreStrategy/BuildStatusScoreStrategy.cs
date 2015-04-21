namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy
{
    using Farfetch.Buildionaire.Domain.Model;

    public class BuildStatusScoreStrategy : IScoreStrategy
    {
        public int GetScore(Build build, Metric metric)
        {
            return metric.Weight * (int)build.Status;
        }
    }
}
