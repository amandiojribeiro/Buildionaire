namespace Farfetch.Buildionaire.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbContext context;

        public DashboardRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DataPair<string, decimal>>> GetCheckinsPerHourAsync()
        {
            return await Task.FromResult(this.context.Database.SqlQuery<DataPair<string, decimal>>(SqlQueries.checkinsPerHourScript).ToList());
        }

        public async Task<IEnumerable<DataPair<string, decimal>>> GetMonthlyScoreAsync()
        {
            try
            {
                return await Task.FromResult(this.context.Database.SqlQuery<DataPair<string, decimal>>(SqlQueries.GetMonthTotalScore).ToList());
            }
            catch (Exception)
            {
                return new List<DataPair<string, decimal>>();
            }
        }

        public async Task<IEnumerable<DataPair<string, decimal>>> GetMonthlyScoreAsync(string dashboardCode)
        {
            return await Task.FromResult(this.context.Database.SqlQuery<DataPair<string, decimal>>(SqlQueries.GetMonthTotalScoreFiltered, dashboardCode).ToList());
        }
    }
}