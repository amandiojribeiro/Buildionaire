namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy
{
    using System;
    using System.Runtime.Remoting.Messaging;

    using Farfetch.Buildionaire.Domain.Model;

    public class ErrorScoreStrategy : IScoreStrategy
    {
        public int GetScore(Build build, Metric metric)
        {
            if (build.Error == null)
            {
                return 0;
            }

            return metric.Weight * build.Error.Total;
        }
    }
}
