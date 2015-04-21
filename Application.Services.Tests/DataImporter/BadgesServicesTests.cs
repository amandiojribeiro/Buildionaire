namespace Farfetch.Buildionaire.Application.Services.DataImporter.Tests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Diagnostics.CodeAnalysis;
    
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class BadgesServicesTests
    {
        [TestMethod()]
        public void CalculateBadgesEmptyRepositoriesTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var badgeTypeRepositoryMock = new Mock<IBadgeTypeRepository>();
            var badgeServiceMock = new Mock<IBadgeService>();

            var badgeService = new BadgesServices(userRepositoryMock.Object, badgeTypeRepositoryMock.Object, badgeServiceMock.Object);
            badgeService.CalculateBadges();

            userRepositoryMock.Verify(e => e.Update(It.IsAny<User>()), Times.Never);
            badgeServiceMock.Verify(e => e.CalculateBadges(It.IsAny<User>(), It.IsAny<List<BadgeType>>()), Times.Never);
        }

        [TestMethod()]
        public void CalculateBadgesEmptyUsersTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var badgeTypeRepositoryMock = new Mock<IBadgeTypeRepository>();
            var badgeServiceMock = new Mock<IBadgeService>();
            badgeTypeRepositoryMock.Setup(e => e.GetAll()).Returns(new List<BadgeType>() { new BadgeType(), new BadgeType() });

            var badgeService = new BadgesServices(userRepositoryMock.Object, badgeTypeRepositoryMock.Object, badgeServiceMock.Object);
            badgeService.CalculateBadges();

            userRepositoryMock.Verify(e => e.Update(It.IsAny<User>()), Times.Never);
            badgeServiceMock.Verify(e => e.CalculateBadges(It.IsAny<User>(), It.IsAny<List<BadgeType>>()), Times.Never);
        }

        [TestMethod()]
        public void CalculateBadgesEmptyBadgesTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var badgeTypeRepositoryMock = new Mock<IBadgeTypeRepository>();
            var badgeServiceMock = new Mock<IBadgeService>();
            
            userRepositoryMock.Setup(e => e.GetAll()).Returns(new List<User>() { new User(), new User() });

            var badgeService = new BadgesServices(userRepositoryMock.Object, badgeTypeRepositoryMock.Object, badgeServiceMock.Object);
            badgeService.CalculateBadges();

            userRepositoryMock.Verify(e => e.Update(It.Is<User>(u => userRepositoryMock.Object.GetAll().Contains(u))), Times.Exactly(2));
            badgeServiceMock.Verify(e => e.CalculateBadges(It.Is<User>(u => userRepositoryMock.Object.GetAll().Contains(u)), It.Is<List<BadgeType>>( l => !l.Any())), Times.Exactly(2));
        }

        [TestMethod()]
        public void CalculateBadgesTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var badgeTypeRepositoryMock = new Mock<IBadgeTypeRepository>();
            var badgeServiceMock = new Mock<IBadgeService>();

            userRepositoryMock.Setup(e => e.GetAll()).Returns(new List<User>() { new User(), new User() });
            badgeTypeRepositoryMock.Setup(e => e.GetAll()).Returns(new List<BadgeType>() { new BadgeType(), new BadgeType() });

            var badgeService = new BadgesServices(userRepositoryMock.Object, badgeTypeRepositoryMock.Object, badgeServiceMock.Object);
            badgeService.CalculateBadges();

            userRepositoryMock.Verify(e => e.Update(It.Is<User>(u => userRepositoryMock.Object.GetAll().Contains(u))), Times.Exactly(2));
            badgeServiceMock.Verify(e => e.CalculateBadges(It.Is<User>(u => userRepositoryMock.Object.GetAll().Contains(u)), It.Is<List<BadgeType>>(l => l.Intersect(badgeTypeRepositoryMock.Object.GetAll()).Count() == 2)), Times.Exactly(2));
        }
    }
}