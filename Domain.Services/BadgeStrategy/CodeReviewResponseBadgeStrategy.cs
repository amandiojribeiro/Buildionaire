namespace Farfetch.Buildionaire.Domain.Services.BadgeStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;
    
    public class CodeReviewResponseBadgeStrategy : BaseBadgeStrategy, IBadgeStrategy
    {
        public void CalculateBadges(Domain.Model.User user, Domain.Model.BadgeType badgeType)
        {
            var value = user.CodeReviews.Count(e => e.Type == CodeReviewType.Response);
            this.CalculateMinThresholdedBadge(user, value, badgeType);
        }
    }
}
