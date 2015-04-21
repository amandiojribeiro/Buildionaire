namespace Farfetch.Buildionaire.Domain.Services
{
    using System.Collections.Generic;
    using Farfetch.Buildionaire.Domain.Model;

    public interface IBadgeService
    {
        void CalculateBadges(User user, List<BadgeType> badgeTypes);
    }
}