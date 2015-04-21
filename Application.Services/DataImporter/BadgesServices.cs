namespace Farfetch.Buildionaire.Application.Services.DataImporter
{
    using System.Linq;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Services;
    
    public class BadgesServices : IBadgesService
    {
        private readonly IUserRepository userRepository;

        private readonly IBadgeTypeRepository badgeTypeRepository;

        private readonly IBadgeService badgeService;

        public BadgesServices(IUserRepository userRepository, IBadgeTypeRepository badgeTypeRepository, IBadgeService badgeService)
        {
            this.userRepository = userRepository;
            this.badgeTypeRepository = badgeTypeRepository;
            this.badgeService = badgeService;
        }

        public void CalculateBadges()
        {
            var users = this.userRepository.GetAll();
            var badgeTypes = this.badgeTypeRepository.GetAll().ToList();
            foreach (var user in users)
            {
                this.badgeService.CalculateBadges(user, badgeTypes);
                this.userRepository.Update(user);
            }
        }
    }
}