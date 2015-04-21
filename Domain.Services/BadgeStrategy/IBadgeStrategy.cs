namespace Farfetch.Buildionaire.Domain.Services.BadgeStrategy
{
    using Farfetch.Buildionaire.Domain.Model;

    public interface IBadgeStrategy
    {
        void CalculateBadges(User user, BadgeType badgeType);
    }
}
