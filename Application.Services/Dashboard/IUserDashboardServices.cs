namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Threading.Tasks;

    public interface IUserDashboardServices
    {
        Task<int> GetPeopleInvolvedAsync();
    }
}
