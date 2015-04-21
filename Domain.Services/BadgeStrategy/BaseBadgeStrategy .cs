namespace Farfetch.Buildionaire.Domain.Services.BadgeStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;
    using Farfetch.Buildionaire.Domain.Model;

    public abstract class BaseBadgeStrategy 
    {
        protected void CalculateMinThresholdedBadge(User user, int value, BadgeType badgeType)
        {
            var badges = badgeType.Badges.Where(e => e.Min < value).ToList();
            foreach (var badge in badges)
            {
                if (badge != null)
                {
                    if (!user.Badges.Any(e => e.Badge.Id == badge.Id))
                    {
                        user.Badges.Add(new UserBadge { Badge = badge, ReceivedDate = DateTime.UtcNow });
                    }
                }
            }
        }
    }
}
