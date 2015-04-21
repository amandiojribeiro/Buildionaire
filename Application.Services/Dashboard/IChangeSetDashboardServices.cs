namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Threading.Tasks;

    public interface IChangesetDashboardServices
    {
        Task<int> GetTopCheckinsAsync();

        Task<string> GetTopCheckinsUserAsync();

        Task<int> GetTotalCheckinsAsync();
    }
}
