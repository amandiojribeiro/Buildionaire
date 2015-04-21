namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Core;

    public class UserDashboardServices : IUserDashboardServices
    {
        private readonly IUserRepository userRepository;

        public UserDashboardServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> GetPeopleInvolvedAsync()
        {
            return await Task.FromResult(this.userRepository.Count());
        }
    }
}
