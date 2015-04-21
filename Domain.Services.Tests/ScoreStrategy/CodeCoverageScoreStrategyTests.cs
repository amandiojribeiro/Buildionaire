namespace Farfetch.Buildionaire.Domain.Services.Tests.ScoreStrategy
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class CodeCoverageScoreStrategyTests
    {
        [TestMethod()]
        public void GetScore_AverageMultipliedByWeight()
        {
            // Arrange
            var target = new CodeCoverageScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded, CodeCoverage = new CodeCoverage { TotalBlocks = 1000, CoveredBlocks = 49 } },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual((int)Math.Round(49 * 100.0 / 1000 * 2), act);
        }

        [TestMethod()]
        public void GetScore_ZeroTotalBlocks()
        {
            // Arrange
            var target = new CodeCoverageScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded, CodeCoverage = new CodeCoverage { TotalBlocks = 0, CoveredBlocks = 49 } },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(0, act);
        }

        [TestMethod()]
        public void GetScore_NullCodeCoverage()
        {
            // Arrange
            var target = new CodeCoverageScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded},
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(0, act);
        }


    }
}