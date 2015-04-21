namespace Data.Repository.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Farfetch.Buildionaire.Data.Repository;
    using Farfetch.Buildionaire.Data.Repository.DomainContext;
    using Farfetch.Buildionaire.Domain.Model;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildRepositoryTests
    {
        #region GetLastBuild
        [TestMethod]
        public void GetLastBuild_WithNoBuilds_ReturnsNull()
        {
            // Arrange
            var data = new List<Build>().AsQueryable();

            var mockContext = BuildBuildContext(data);

            var target = new BuildRepository(mockContext.Object);

            /// Act
            var act = target.GetLastBuild();

            // Assert
            Assert.IsNull(act);
        }

        [TestMethod]
        public void GetLastBuild_WithBuilds_ReturnsLastBuild()
        {
            // Arrange
            var data = new List<Build>
                           {
                               new Build { BuildNumber = "bn1", Id = 1, CreatedAt = new DateTime(2014, 01, 01) },
                               new Build { BuildNumber = "bn2", Id = 2, CreatedAt = new DateTime(2014, 01, 02) }
                           }
                .AsQueryable();

            var mockContext = BuildBuildContext(data);

            var target = new BuildRepository(mockContext.Object);

            /// Act
            var act = target.GetLastBuild();

            // Assert
            Assert.AreEqual(2, act.Id);
        }
        #endregion


        #region GetLastFailedBuildAsync
        [TestMethod]
        public void GetLastFailedBuildAsync_WithNoFailedBuilds_ReturnsNull()
        {
            // Arrange
            var data = new List<Build>
                           {
                               new Build { BuildNumber = "bn1", Id = 1, CreatedAt = new DateTime(2014, 01, 01), Status = BuildStatus.Succeeded },
                               new Build { BuildNumber = "bn2", Id = 2, CreatedAt = new DateTime(2014, 01, 02), Status = BuildStatus.Succeeded }
                           }
                .AsQueryable();

            var mockContext = BuildBuildContext(data);

            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetLastFailedBuildAsync().Result;

            // Assert
            Assert.IsNull(act);
        }

        [TestMethod]
        public void GetLastFailedBuildAsync_WithFailedBuilds_ReturnsLastBuild()
        {
            // Arrange
            var data = new List<Build>
                           {
                               new Build { BuildNumber = "bn0", Id = 0, CreatedAt = new DateTime(2014, 01, 01), Status = BuildStatus.PartiallySucceeded },
                               new Build { BuildNumber = "bn1", Id = 1, CreatedAt = new DateTime(2014, 01, 02), Status = BuildStatus.Failed },
                               new Build { BuildNumber = "bn2", Id = 2, CreatedAt = new DateTime(2014, 01, 03), Status = BuildStatus.Succeeded }
                           }
                .AsQueryable();


            var mockContext = BuildBuildContext(data);

            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetLastFailedBuildAsync().Result;

            // Assert
            Assert.AreEqual(1, act.Id);
        }
        #endregion


        #region GetHighlitedBuilds
        [TestMethod]
        public void GetHighlitedBuilds_OnTop_GetHighestScoreBuilds()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetHighlitedBuilds(true, 1).Result;

            // Assert
            Assert.AreEqual(1, act.Count());
            Assert.AreEqual(4, act.First().Id);
        }

        [TestMethod]
        public void GetHighlitedBuilds_OnTopFalse_GetLowestScoreBuilds()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetHighlitedBuilds(false, 1).Result;

            // Assert
            Assert.AreEqual(1, act.Count());
            Assert.AreEqual(2, act.First().Id);
        }

        [TestMethod]
        public void GetHighlitedBuilds_OnTakeTwo_GetTwoBuilds()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetHighlitedBuilds(true, 2).Result;

            // Assert
            Assert.AreEqual(2, act.Count());
        }

        [TestMethod]
        public void GetHighlitedBuilds_OnTakeTwo_GetTwoBuildsFromDifferentProjects()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetHighlitedBuilds(true, 2).Result;

            // Assert
            Assert.AreNotEqual(act[0].Project.Name, act[1].Project.Name);
        }

        [TestMethod]
        public void GetHighlitedBuilds_OnFilterByDashboard_GetDashboardProject()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetHighlitedBuilds(true, 1, "DS1").Result;

            // Assert
            Assert.AreNotEqual("project1", act[0].Project.Name);
        }
        #endregion


        #region GetHighlitedBuilds
        [TestMethod]
        public void GetMonthlyHighlitedBuilds_OnTop_GetHighestScoreBuilds()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetMonthHighlitedBuilds(2014, 1, true, 1, null).Result;

            // Assert
            Assert.AreEqual(1, act.Count());
            Assert.AreEqual(14, act.First().Id);
        }

        [TestMethod]
        public void GetMonthlyHighlitedBuilds_OnTopFalse_GetLowestScoreBuilds()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetMonthHighlitedBuilds(2014, 1, false, 1, null).Result;

            // Assert
            Assert.AreEqual(1, act.Count());
            Assert.AreEqual(12, act.First().Id);
        }

        [TestMethod]
        public void GetMonthlyHighlitedBuilds_OnTakeTwo_GetTwoBuilds()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetMonthHighlitedBuilds(2014, 1, true, 2, null).Result;

            // Assert
            Assert.AreEqual(2, act.Count());
        }

        [TestMethod]
        public void GetMonthlyHighlitedBuilds_OnTakeTwo_GetTwoBuildsFromDifferentProjects()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetMonthHighlitedBuilds(2014, 1, true, 2, null).Result;

            // Assert
            Assert.AreNotEqual(act[0].Project.Name, act[1].Project.Name);
        }

        [TestMethod]
        public void GetMonthlyHighlitedBuilds_OnFilterByDashboard_GetDashboardProject()
        {
            // Arrange
            var mockContext = BuildHighlitedBuildContext();
            var target = new BuildRepository(mockContext.Object);

            // Act
            var act = target.GetMonthHighlitedBuilds(2014, 1, true, 1, "DS1").Result;

            // Assert
            Assert.AreNotEqual("project1", act[0].Project.Name);
        }
        #endregion
        
        private static Mock<BuildionaireDomainContext> BuildHighlitedBuildContext()
        {
            var project1 = new Project { Id = 1, Name = "Project1" };
            var project2 = new Project { Id = 2, Name = "Project2" };
            var dashboad = new Dashboard { Id = 1, Code = "DS1", Projects = new List<Project> { project1 } };

            project1.Dashboards = new List<Dashboard> { dashboad };
            project2.Dashboards = new List<Dashboard>();
            var data =
                new List<Build>
                    {
                        new Build
                            {
                                BuildNumber = "bn1",
                                Id = 11,
                                CompletedAt = new DateTime(2014, 01, 01).AddHours(1),
                                TotalScore = 9,
                                Project = project1
                            },
                        new Build
                            {
                                BuildNumber = "bn2",
                                Id = 12,
                                CompletedAt = new DateTime(2014, 01, 02),
                                TotalScore = 8,
                                Project = project1
                            },
                        new Build
                            {
                                BuildNumber = "bn3",
                                Id = 13,
                                CompletedAt = new DateTime(2014, 01, 01).AddHours(2),
                                TotalScore = 10,
                                Project = project2
                            },
                        new Build
                            {
                                BuildNumber = "bn4",
                                Id = 14,
                                CompletedAt = new DateTime(2014, 01, 02),
                                TotalScore = 12,
                                Project = project2
                            },
                            new Build
                            {
                                BuildNumber = "bn5",
                                Id = 1,
                                CompletedAt = new DateTime(2014, 02, 01).AddHours(1),
                                TotalScore = 9,
                                Project = project1
                            },
                        new Build
                            {
                                BuildNumber = "bn6",
                                Id = 2,
                                CompletedAt = new DateTime(2014, 02, 02),
                                TotalScore = 8,
                                Project = project1
                            },
                        new Build
                            {
                                BuildNumber = "bn7",
                                Id = 3,
                                CompletedAt = new DateTime(2014, 02, 01).AddHours(2),
                                TotalScore = 10,
                                Project = project2
                            },
                        new Build
                            {
                                BuildNumber = "bn8",
                                Id = 4,
                                CompletedAt = new DateTime(2014, 02, 02),
                                TotalScore = 12,
                                Project = project2
                            }
                    }.AsQueryable();

            var mockContext = BuildBuildContext(data);
            return mockContext;
        }

        private static Mock<BuildionaireDomainContext> BuildBuildContext(IQueryable<Build> data)
        {
            var mockSet = new Mock<DbSet<Build>>();
            mockSet.As<IQueryable<Build>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Build>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Build>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Build>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<BuildionaireDomainContext>();
            //mockContext.Setup(c => c.Builds).Returns(mockSet.Object);
            mockContext.Setup(c => c.Set<Build>()).Returns(mockSet.Object);
            return mockContext;
        }
    }
}
