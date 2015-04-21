namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy
{
    using Farfetch.Buildionaire.Domain.Model;

    public interface IScoreStrategy
    {
        int GetScore(Build build, Metric metric);
    }
}
