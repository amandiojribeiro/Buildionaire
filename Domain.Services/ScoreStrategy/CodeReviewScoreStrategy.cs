namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy
{
    using System;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Model;

    public class CodeReviewScoreStrategy : IScoreStrategy
    {
        public int GetScore(Build build, Metric metric)
        {
            int codeReviews = build.CodeReviews != null ? build.CodeReviews.Count() : 0;

            return metric.Weight * codeReviews;
        }
    }
}
