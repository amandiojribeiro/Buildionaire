namespace Farfetch.Buildionaire.Application.Services.DataImporter.Tests
{
    using Farfetch.Buildionaire.Application.Services.DataImporter;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;

    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class ImportChangesetServicesTests
    {
        [TestMethod()]
        public void ImportChangesetsEmptyChangeSetsTest()
        {
            var changesetRepositoryMock = new Mock<IChangesetRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGatewayMock = new Mock<IChangesetGateway>();
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var importChangesetServices = new ImportChangesetServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGatewayMock.Object, projectRepositoryMock.Object);
            importChangesetServices.ImportChangesets();
            changesetRepositoryMock.Verify(e => e.Add(It.IsAny<ChangeSet>()), Times.Never);
        }

        [TestMethod()]
        public void ImportChangesetsRepeatedImportTest()
        {
            var changesetRepositoryMock = new Mock<IChangesetRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<IChangesetGateway>();
            var projectRepositoryMock = new Mock<IProjectRepository>();

            changesetGateway.Setup(e => e.GetChangesets(It.IsAny<DateTime>()))
                .Returns(new List<ChangeSet>() { new ChangeSet { ExternalChangesetId = 1 } });
            changesetRepositoryMock.Setup(e => e.Exists(It.IsAny<Expression<Func<ChangeSet, bool>>>()))
                .Returns<Expression<Func<ChangeSet, bool>>>(x => x.Compile().Invoke(new ChangeSet { ExternalChangesetId = 1 }));
            var importChangesetServices = new ImportChangesetServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object, projectRepositoryMock.Object);
            importChangesetServices.ImportChangesets();
            changesetRepositoryMock.Verify(e => e.Add(It.IsAny<ChangeSet>()), Times.Never);
        }

        [TestMethod()]
        public void ImportChangesetsLastDateFirstCallTest()
        {
            var changesetRepositoryMock = new Mock<IChangesetRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<IChangesetGateway>();
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var importChangesetServices = new ImportChangesetServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object, projectRepositoryMock.Object);
            importChangesetServices.ImportChangesets();

            changesetGateway.Verify(e => e.GetChangesets(It.Is<DateTime>(c => c == new DateTime(2014, 1, 1))), Times.Once);
        }

        [TestMethod()]
        public void ImportChangesetsLastDateTest()
        {
            var changesetRepositoryMock = new Mock<IChangesetRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<IChangesetGateway>();
            var projectRepositoryMock = new Mock<IProjectRepository>();

            changesetRepositoryMock.Setup(e => e.GetLast()).Returns(new ChangeSet { CreatedAt = new DateTime(2015, 2, 1) });

            var importChangesetServices = new ImportChangesetServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object, projectRepositoryMock.Object);
            importChangesetServices.ImportChangesets();

            changesetGateway.Verify(e => e.GetChangesets(It.Is<DateTime>(c => c == new DateTime(2015, 2, 1))), Times.Once);
        }

        [TestMethod()]
        public void ImportChangesetsTest()
        {
            var changesetRepositoryMock = new Mock<IChangesetRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<IChangesetGateway>();
            var projectRepositoryMock = new Mock<IProjectRepository>();

            changesetGateway.Setup(e => e.GetChangesets(It.IsAny<DateTime>()))
                .Returns(new List<ChangeSet>() { new ChangeSet { ExternalChangesetId = 2, User = new User { Name = "UserName" } } });

            changesetRepositoryMock.Setup(e => e.Exists(It.IsAny<Expression<Func<ChangeSet, bool>>>()))
                .Returns<Expression<Func<ChangeSet, bool>>>(x => x.Compile().Invoke(new ChangeSet { ExternalChangesetId = 1 }));

            userRepositoryMock.Setup(e => e.Find(It.Is<Expression<Func<User, bool>>>(x => x.Compile().Invoke(new User { Name = "UserName" })), null))
                .Returns(new User { Name = "UserName" });

            var importChangesetServices = new ImportChangesetServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object, projectRepositoryMock.Object);
            importChangesetServices.ImportChangesets();
            changesetRepositoryMock.Verify(e => e.Add(It.Is<ChangeSet>(c => c.User.Name == "UserName" && c.ExternalChangesetId == 2)), Times.Once);
        }
    }
}