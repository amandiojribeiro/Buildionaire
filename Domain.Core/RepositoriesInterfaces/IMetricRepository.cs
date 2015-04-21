namespace Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces
{
    using Farfetch.Buildionaire.Domain.Model;
    using SharpRepository.Repository;

    public interface IMetricRepository : IRepository<Metric, int>
    {
    }
}
