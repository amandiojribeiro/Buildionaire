namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters;

    public class BadgeDashboardServices : IBadgeDashboardServices
    {
        private readonly IUserRepository userRepository;
        private readonly IUserBadgeRepository userBadgeRepository;

        public BadgeDashboardServices(IUserRepository userRepository, IUserBadgeRepository userBadgeRepository)
        {
            this.userRepository = userRepository;
            this.userBadgeRepository = userBadgeRepository;
        }

        public Task<IEnumerable<RankingBadgeOwnersItemDto>> GetTopBadgeOwnersAsync()
        {
            var topBadgeOwner = this.GetTopBadgeOwners();
            var listTopBadgeOwners = TypeAdapterHelper.Adapt<List<RankingBadgeOwnersItemDto>>(topBadgeOwner);
            return Task.FromResult(listTopBadgeOwners.AsEnumerable());
        }

        public Task<List<UserWithBadgesDto>> GetUsersWithBadgesAsync()
        {
            var users = this.userRepository.FindAll(e => e.Badges.Count() > 6);
            return Task.FromResult(TypeAdapterHelper.Adapt<List<UserWithBadgesDto>>(users));
        }

        public Task<int> GetTotalBadgesAsync()
        {
            return Task.FromResult(this.userBadgeRepository.Count());
        }

        private IQueryable<User> GetTopBadgeOwners()
        {
            return this.userRepository.AsQueryable().OrderByDescending(x => x.Badges.Count).Take(3);
        }
    }
}
