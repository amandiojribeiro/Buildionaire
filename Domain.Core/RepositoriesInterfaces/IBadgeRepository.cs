namespace Farfetch.Buildionaire.Domain.Core
{
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.Repository;

    public interface IBadgeRepository : IRepository<Badge, int>
    {
    }
}