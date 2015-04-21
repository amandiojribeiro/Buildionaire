namespace Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Model;

    public interface IDashboardRepository
    {
        Task<IEnumerable<DataPair<string, decimal>>> GetCheckinsPerHourAsync();

        Task<IEnumerable<DataPair<string, decimal>>> GetMonthlyScoreAsync();

        Task<IEnumerable<DataPair<string, decimal>>> GetMonthlyScoreAsync(string dashboardCode);
    }
}
