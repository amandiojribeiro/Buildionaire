namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class BadgeRepository : EfRepository<Badge, int>, IBadgeRepository
    {
        public BadgeRepository(DbContext context)
            : base(context)
        {
        }
    }
}