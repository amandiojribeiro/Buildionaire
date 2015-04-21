namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class UserBadgeRepository : EfRepository<UserBadge, int>, IUserBadgeRepository
    {
        public UserBadgeRepository(DbContext context)
            : base(context)
        {
        }
    }
}
