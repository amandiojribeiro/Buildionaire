namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Threading.Tasks;

    public interface IScoreDashboardServices
    {
        Task<int> GetTotalBuildCoinsAsync();
    }
}
