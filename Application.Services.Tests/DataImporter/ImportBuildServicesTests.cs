namespace Application.Services.Tests.DataImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Farfetch.Buildionaire.Application.Dto;
    using Farfetch.Buildionaire.Application.Services.DataImporter;
    using Farfetch.Buildionaire.Application.Services.Management;
    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters;
    using Farfetch.Buildionaire.Infrastructure.CrossCutting.Adapters.Automapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ImportBuildServicesTests
    {
        [TestInitialize]
        public void Setup()
        {
            Assembly.Load("Farfetch.Buildionaire.Application.Services");
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());
        }

        [TestMethod]
        public void GetLastNotImportedBuilds_AfterLastBuildDate_SaveInBuildRepository()
        { 
            ///Arrange
            var fakeProject = new Project { Name = "fakeProject", Id = 1 };
            var fakeBuildsList = new List<Build> 
            {
                new Build { BuildNumber = "123", Name = "foo", CreatedAt = new DateTime(2015,3,5), Project = fakeProject },
                new Build { BuildNumber = "124", Name = "foa", CreatedAt = new DateTime(2015,3,6), Project = fakeProject }
            };
            var fakeGatewayBuildsList = new List<Build> 
            {
                new Build { BuildNumber = "1231", Name = "foo", CreatedAt = new DateTime(2015,3,8), Project = fakeProject },
                new Build { BuildNumber = "1241", Name = "foa", CreatedAt = new DateTime(2015,3,7), Project = fakeProject }
            };
            var fakeMetricList = new List<Metric>
            {
                new Metric{ Code="XPTO", Description="For testing purposes", Id=1, Weight=5 },
                new Metric{ Code="XPTA", Description="For testing purposes", Id=1, Weight=5 },
            };
            var fakeBuildScoreDetailsList = new List<BuildScoreDetail>
            {
                new BuildScoreDetail { Id=1, Score=1 },
                new BuildScoreDetail { Id=2, Score=2 }
            };
            var loadHistorySince = new DateTime(2015, 1, 1);
            var buildRepositoryMoq = new Moq.Mock<IBuildRepository>();
            var metricRepositoryMoq = new Moq.Mock<IMetricRepository>();
            var projectRepositoryMoq = new Moq.Mock<IProjectRepository>();
            var buildGatewayMoq = new Moq.Mock<IBuildGateway>();
            var scoreServiceMoq = new Moq.Mock<IScoreService>();

            buildRepositoryMoq.Setup(x => x.GetLastBuild()).Returns(fakeBuildsList.OrderByDescending(u => u.CreatedAt).FirstOrDefault());
            buildRepositoryMoq.Setup(x => x.Add(It.IsAny<Build>())).Verifiable();
            metricRepositoryMoq.Setup(x => x.GetAll()).Returns(fakeMetricList);
            projectRepositoryMoq.Setup(x => x.Find(It.IsAny<Expression<Func<Project, bool>>>(), null)).Returns(fakeProject);
            buildGatewayMoq.Setup(x => x.GetBuilds(It.IsAny<DateTime>())).Returns(fakeGatewayBuildsList);
            scoreServiceMoq.Setup(x => x.CalculateScoreDetail(It.IsAny<Build>(), It.IsAny<List<Metric>>())).Returns(fakeBuildScoreDetailsList);
            
            var importBuildServices = new ImportBuildServices(buildGatewayMoq.Object,
                buildRepositoryMoq.Object, scoreServiceMoq.Object, metricRepositoryMoq.Object, projectRepositoryMoq.Object, loadHistorySince);

            ///Act
            importBuildServices.ImportBuilds();

            ///Assert
            buildRepositoryMoq.Verify();
        }

        [TestMethod]
        public void ImportedAllBuilds_FromTheBeginning_SaveInBuildRepository()
        {
            ///Arrange
            var fakeProject = new Project { Name = "fakeProject", Id = 1 };
            var fakeGatewayBuildsList = new List<Build> 
            {
                new Build { BuildNumber = "1231", Name = "foo", CreatedAt = new DateTime(2015,3,8), Project = fakeProject },
                new Build { BuildNumber = "1241", Name = "foa", CreatedAt = new DateTime(2015,3,7), Project = fakeProject }
            };
            var fakeMetricList = new List<Metric>
            {
                new Metric{ Code="XPTO", Description="For testing purposes", Id=1, Weight=5 },
                new Metric{ Code="XPTA", Description="For testing purposes", Id=1, Weight=5 },
            };
            var fakeBuildScoreDetailsList = new List<BuildScoreDetail>
            {
                new BuildScoreDetail { Id=1, Score=1 },
                new BuildScoreDetail { Id=2, Score=2 }
            };
            var loadHistorySince = new DateTime(2015, 1, 1);
            var buildRepositoryMoq = new Moq.Mock<IBuildRepository>();
            var metricRepositoryMoq = new Moq.Mock<IMetricRepository>();
            var projectRepositoryMoq = new Moq.Mock<IProjectRepository>();
            var buildGatewayMoq = new Moq.Mock<IBuildGateway>();
            var scoreServiceMoq = new Moq.Mock<IScoreService>();

            buildRepositoryMoq.Setup(x => x.Add(It.IsAny<Build>())).Verifiable();
            metricRepositoryMoq.Setup(x => x.GetAll()).Returns(fakeMetricList);
            projectRepositoryMoq.Setup(x => x.Find(It.IsAny<Expression<Func<Project, bool>>>(), null)).Returns(fakeProject);
            buildGatewayMoq.Setup(x => x.GetBuilds(It.IsAny<DateTime>())).Returns(fakeGatewayBuildsList);
            scoreServiceMoq.Setup(x => x.CalculateScoreDetail(It.IsAny<Build>(), It.IsAny<List<Metric>>())).Returns(fakeBuildScoreDetailsList);

            var importBuildServices = new ImportBuildServices(buildGatewayMoq.Object,
                buildRepositoryMoq.Object, scoreServiceMoq.Object, metricRepositoryMoq.Object, projectRepositoryMoq.Object, loadHistorySince);

            ///Act
            importBuildServices.ImportBuilds();

            ///Assert
            buildRepositoryMoq.Verify();
        }
    }
}
