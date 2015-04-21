namespace Farfetch.Buildionaire.Application.Services.Dashboard
{
    using System.Linq;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    public class ChangesetDashboardServices : IChangesetDashboardServices
    {
        private readonly IUserRepository userRepository;
        private readonly IChangesetRepository changesetRepository;

        public ChangesetDashboardServices(IUserRepository userRepository, IChangesetRepository changesetRepository)
        {
            this.userRepository = userRepository;
            this.changesetRepository = changesetRepository;
        }

        public async Task<int> GetTopCheckinsAsync()
        {
            User user = await this.userRepository.GetTopCheckinUserAsync();
            return user.ChangeSets.Count();
        }

        public async Task<string> GetTopCheckinsUserAsync()
        {
            var user = await this.userRepository.GetTopCheckinUserAsync();
            return user.Name;
        }

        public async Task<int> GetTotalCheckinsAsync()
        {
            return await Task.FromResult(this.changesetRepository.Count());
        }
    }
}
