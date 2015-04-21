namespace Farfetch.Buildionaire.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;
    using Farfetch.Buildionaire.Domain.Services.BadgeStrategy;

    public class BadgeService : IBadgeService
    {
        private BadgeStrategyFactory badgeStrategyFactory;

        public BadgeService(BadgeStrategyFactory badgeStrategyFactory)
        {
            this.badgeStrategyFactory = badgeStrategyFactory;
        }

        public void CalculateBadges(User user, List<BadgeType> badgeTypes)
        {
            foreach (var badgeType in badgeTypes)
            {
                this.badgeStrategyFactory.GetBadgeStrategy(badgeType).CalculateBadges(user, badgeType);
            }
        }
    }
}