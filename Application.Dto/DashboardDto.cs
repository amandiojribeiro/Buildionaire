namespace Farfetch.Buildionaire.Application.Dto
{
    using System;
    using System.Collections.Generic;

    public class DashboardDto
    {
        public IEnumerable<RankingBuildItemDto> TopBuilds { get; set; }

        public IEnumerable<RankingBuildItemDto> BottomBuilds { get; set; }

        public IEnumerable<RankingBuildItemDto> MonthTopBuilds { get; set; }

        public IEnumerable<RankingBuildItemDto> MonthBottomBuilds { get; set; }

        public IEnumerable<Tuple<string, string>> RandomData { get; set; }

        public IEnumerable<RankingReviewerItemDto> TopCodeReviewers { get; set; }

        public IEnumerable<RankingReviewerItemDto> MonthTopCodeReviewers { get; set; }

        public IEnumerable<RankingBadgeOwnersItemDto> TopBadgesOwners { get; set; }

        public IEnumerable<Tuple<string, decimal>> CheckinsByHour { get; set; }

        public IEnumerable<Tuple<string, decimal>> MonthlyScore { get; set; }

        public IEnumerable<UserWithBadgesDto> AllUSersWithBadges { get; set; }
    }
}
