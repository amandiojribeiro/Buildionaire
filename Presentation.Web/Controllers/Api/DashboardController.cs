namespace Farfetch.Buildionaire.Presentation.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Application.Services.Dashboard;

    public class DashboardController : ApiController
    {
        private readonly IBuildDashboardServices buildDashboardServices;
        private readonly ICodeReviewDashboardServices codeReviewDashboardServices;
        private readonly IChangesetDashboardServices changesetDashboardServices;
        private readonly IBadgeDashboardServices badgeDashboardServices;
        private readonly IUserDashboardServices userDashboardServices;
        private readonly IScoreDashboardServices scoreDashboardServices;

        public DashboardController(IBuildDashboardServices buildDashboardServices, ICodeReviewDashboardServices codeReviewDashboardServices, IChangesetDashboardServices changesetDashboardServices, IBadgeDashboardServices badgeDashboardServices, IUserDashboardServices userDashboardServices, IScoreDashboardServices scoreDashboardServices)
        {
            this.buildDashboardServices = buildDashboardServices;
            this.codeReviewDashboardServices = codeReviewDashboardServices;
            this.changesetDashboardServices = changesetDashboardServices;
            this.badgeDashboardServices = badgeDashboardServices;
            this.userDashboardServices = userDashboardServices;
            this.scoreDashboardServices = scoreDashboardServices;
        }

        public async Task<DashboardDto> Get()
        {
            var topbuildsTask = this.buildDashboardServices.GetTopBuildsAsync();
            var monthTopBuildsTask = this.buildDashboardServices.GetMonthTopBuildsAsync();
            var bottomBuildsTask = this.buildDashboardServices.GetBottomBuildsAsync();
            var monthBottomBuildsTask = this.buildDashboardServices.GetMonthBottomBuildsAsync();
            var monthlyScoreTask = this.buildDashboardServices.GetMonthlyScoreAsync();
            var checkinsByHourTask = this.buildDashboardServices.GetBuildHourDistributionAsync(); // todo: SO rename and replace
            var topReviewersTask = this.codeReviewDashboardServices.GetTopCodeReviewersAsync();
            var monthTopReviewersTask = this.codeReviewDashboardServices.GetMonthTopCodeReviewersAsync();
            var topBadgesOwnersTask = this.badgeDashboardServices.GetTopBadgeOwnersAsync();
            var usersWithBadgesTask = this.badgeDashboardServices.GetUsersWithBadgesAsync();
            var randomDataTask = this.GetRandomDataAsync();


            return new DashboardDto
                           {
                               TopBuilds = await topbuildsTask,
                               MonthTopBuilds = await monthTopBuildsTask,
                               BottomBuilds = await bottomBuildsTask,
                               MonthBottomBuilds = await monthBottomBuildsTask,
                               MonthlyScore = await monthlyScoreTask,
                               CheckinsByHour = await checkinsByHourTask,
                               TopCodeReviewers = await topReviewersTask,
                               MonthTopCodeReviewers = await monthTopReviewersTask,
                               TopBadgesOwners = await topBadgesOwnersTask,
                               AllUSersWithBadges = await usersWithBadgesTask,
                               RandomData = await randomDataTask
                           };
        }

        public async Task<DashboardDto> Get(string id)
        {
            var topbuildsTask = this.buildDashboardServices.GetTopBuildsAsync(id);
            var monthTopBuildsTask = this.buildDashboardServices.GetMonthTopBuildsAsync(id);

            var bottomBuildsTask = this.buildDashboardServices.GetBottomBuildsAsync(id);
            var monthBottomBuildsTask = this.buildDashboardServices.GetMonthBottomBuildsAsync(id);
            var monthlyScoreTask = this.buildDashboardServices.GetMonthlyScoreAsync(id);
            var checkinsByHourTask = this.buildDashboardServices.GetBuildHourDistributionAsync(); // todo: SO rename and replace

            return new DashboardDto
                       {
                           TopBuilds = await topbuildsTask,
                           MonthTopBuilds = await monthTopBuildsTask,
                           BottomBuilds = await bottomBuildsTask,
                           MonthBottomBuilds = await monthBottomBuildsTask,
                           MonthlyScore = await monthlyScoreTask,
                           CheckinsByHour = await checkinsByHourTask
                       };
        }

        protected async Task<IEnumerable<Tuple<string, string>>> GetRandomDataAsync()
        {
            return new List<Tuple<string, string>>
                                 {
                                     await this.GetLastFailedBuildRandomData(),
                                     await this.GetTotalCheckinsRandomData(),
                                     await this.GetTopCheckinUserRandomData(),
                                     await this.GetTopCheckinsCountRandomData(),
                                     await this.GetTotalBuildCoinsRandomData(),
                                     await this.GetBadgesEarnedRandomData(),
                                     await this.GetPeopleInvolvedRandomData(),
                                     await this.GetBuildsMonitoredRandomData()
                                 };
        }

        private async Task<Tuple<string, string>> GetLastFailedBuildRandomData()
        {
            var lastFailedTask = this.buildDashboardServices.GetLastFailedBuildAsync();

            var lastFailedBuild = await lastFailedTask ?? new RankingBuildItemDto { BuildDate = new DateTime(2014, 1, 1) };

            return new Tuple<string, string>(Constants.DateSinceLastIncident, string.Format("{0:yyyy-MM-dd}", lastFailedBuild.BuildDate));
        }

        private async Task<Tuple<string, string>> GetTopCheckinUserRandomData()
        {
            var totalcheckinUserTask = this.changesetDashboardServices.GetTopCheckinsUserAsync();

            var userName = await totalcheckinUserTask;

            return new Tuple<string, string>(Constants.TopCheckinsUser, userName);
        }

        private async Task<Tuple<string, string>> GetTopCheckinsCountRandomData()
        {
            var totalcheckinUserTask = this.changesetDashboardServices.GetTopCheckinsAsync();

            var topcheckinsCount = await totalcheckinUserTask;

            return new Tuple<string, string>(Constants.TopCheckinsCount, topcheckinsCount.ToString(CultureInfo.CurrentUICulture));
        }

        private async Task<Tuple<string, string>> GetTotalCheckinsRandomData()
        {
            var totalcheckinUserTask = this.changesetDashboardServices.GetTotalCheckinsAsync();

            int totalCheckins = await totalcheckinUserTask;

            return new Tuple<string, string>(Constants.TotalCheckins, totalCheckins.ToString(CultureInfo.CurrentUICulture));
        }

        private async Task<Tuple<string, string>> GetBadgesEarnedRandomData()
        {
            var task = this.badgeDashboardServices.GetTotalBadgesAsync();

            int total = await task;

            return new Tuple<string, string>(Constants.BadgesEarned, total.ToString(CultureInfo.CurrentUICulture));
        }

        private async Task<Tuple<string, string>> GetBuildsMonitoredRandomData()
        {
            var task = this.buildDashboardServices.GetTotalBuildsAsync();

            int total = await task;

            return new Tuple<string, string>(Constants.BuildsMonitored, total.ToString(CultureInfo.CurrentUICulture));
        }

        private async Task<Tuple<string, string>> GetPeopleInvolvedRandomData()
        {
            var task = this.userDashboardServices.GetPeopleInvolvedAsync();

            var total = await task;

            return new Tuple<string, string>(Constants.PeopleInvolved, total.ToString(CultureInfo.CurrentUICulture));
        }

        private async Task<Tuple<string, string>> GetTotalBuildCoinsRandomData()
        {
            var task = this.scoreDashboardServices.GetTotalBuildCoinsAsync();

            var total = await task;

            return new Tuple<string, string>(Constants.BuildCoins, total.ToString(CultureInfo.CurrentUICulture));
        }

        private static class Constants
        {
            public const string DateSinceLastIncident = "dateSinceLastIncident";
            public const string TopCheckinsUser = "topCheckins";
            public const string TopCheckinsCount = "topCheckinsNum";
            public const string TotalCheckins = "totalCheckins";
            public const string BadgesEarned = "badgesEarned";
            public const string BuildsMonitored = "buildsMonitored";
            public const string PeopleInvolved = "peopleInvolved";
            public const string BuildCoins = "buildcoins";
        }
    }
}
