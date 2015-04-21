namespace Farfetch.Buildionaire.Domain.Core
{
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.Repository;

    public interface IBadgeTypeRepository : IRepository<BadgeType, int>
    {
    }
}