namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Application.Dto;

    public interface IBadgeDashboardServices
    {
        Task<IEnumerable<RankingBadgeOwnersItemDto>> GetTopBadgeOwnersAsync();

        Task<List<UserWithBadgesDto>> GetUsersWithBadgesAsync();

        Task<int> GetTotalBadgesAsync();
    }
}
