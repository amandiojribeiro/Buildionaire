namespace Farfetch.Buildionaire.Domain.Services.BadgeStrategy
{
    using System;
    using System.Collections.Generic;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services.BadgeStrategy;
    
    public class BadgeStrategyFactory
    {
        private readonly Dictionary<string, IBadgeStrategy> availableStrategies;

        public BadgeStrategyFactory()
        {
            this.availableStrategies = new Dictionary<string, IBadgeStrategy>
                                           {
                                               { BadgeType.Checkin, new CheckinBadgeStrategy() },
                                               { BadgeType.CodeReviewRequest, new CodeReviewRequestBadgeStrategy() },
                                               { BadgeType.CodeReviewResponse, new CodeReviewResponseBadgeStrategy() },
                                           };
        }

        public virtual IBadgeStrategy GetBadgeStrategy(BadgeType badgeType)
        {
            if (!this.availableStrategies.ContainsKey(badgeType.Code))
            {
                throw new ArgumentException("Badge type not supported", badgeType.Code);
            }

            return this.availableStrategies[badgeType.Code];
        }
    }
}