namespace Farfetch.Buildionaire.Domain.Services.BadgeStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;
    using Farfetch.Buildionaire.Domain.Model;

    public class CheckinBadgeStrategy : BaseBadgeStrategy, IBadgeStrategy
    {
        public void CalculateBadges(Domain.Model.User user, Domain.Model.BadgeType badgeType)
        {
            var value = user.ChangeSets.Count();
            this.CalculateMinThresholdedBadge(user, value, badgeType);
        }
    }
}
