namespace Application.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Farfetch.Buildionaire.Application.Dto;
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
    public class BuildServicesTests
    {
        [TestInitialize]
        public void Setup()
        {
            Assembly.Load("Farfetch.Buildionaire.Application.Services");
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());          
        }

        [TestMethod]
        public void GetBuild_WhenCalled_CorrectBuildIsReturn_Test()
        {
            ///Arrange
            var fakeBuild = new Build { BuildNumber = "123", Name = "foo" };
            
            var buildRepositoryMoq = new Moq.Mock<IBuildRepository>();

            buildRepositoryMoq.Setup(x => x.Find(It.IsAny<Expression<Func<Build, bool>>>(), null))
                .Returns(fakeBuild);

            var buildServices = new BuildServices(buildRepositoryMoq.Object);

            ///Act
            var buildDto = buildServices.GetBuild("foo");

            ///Assert
            Assert.IsNotNull(buildDto);
            Assert.IsInstanceOfType(buildDto, typeof(BuildDto));
            Assert.AreEqual(buildDto.BuildNumber, fakeBuild.BuildNumber);
            Assert.AreEqual(buildDto.Name, fakeBuild.Name);
        }

        [TestMethod]
        public void GetAllBuild_WhenCalled_CorrectListBuildIsReturn_Test()
        {
            ///Arrange
            var fakeBuilds = new[] 
            { 
                new Build { BuildNumber = "123", Name = "foo" },
                new Build { BuildNumber = "124", Name = "foa" }
            };

            var buildRepositoryMoq = new Moq.Mock<IBuildRepository>();

            buildRepositoryMoq.Setup(x => x.GetAll())
                .Returns(fakeBuilds);

            var buildServices = new BuildServices(buildRepositoryMoq.Object);

            ///Act
            var buildDtoList = buildServices.GetBuilds();

            ///Assert
            Assert.IsNotNull(buildDtoList);
            Assert.IsInstanceOfType(buildDtoList, typeof(List<BuildDto>));
            Assert.AreEqual(buildDtoList.Count(), 2);
        }
    }
}
