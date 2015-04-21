namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class BadgeTypeRepository : EfRepository<BadgeType, int>, IBadgeTypeRepository
    {
        public BadgeTypeRepository(DbContext context)
            : base(context)
        {
        }
    }
}