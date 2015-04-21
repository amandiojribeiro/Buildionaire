namespace Farfetch.Buildionaire.Domain.Services
{
    using System.Collections.Generic;

    using Farfetch.Buildionaire.Domain.Model;

    public interface IScoreService
    {
        BuildScoreDetail CalculateScoreDetail(Build build, Metric metric);

        IEnumerable<BuildScoreDetail> CalculateScoreDetail(Build build, IEnumerable<Metric> metric);
    }
}
