namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;

    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class MetricRepository : EfRepository<Metric, int>, IMetricRepository
    {
        public MetricRepository(DbContext context)
            : base(context)
        {
        }
    }
}
