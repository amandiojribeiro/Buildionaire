using Farfetch.Buildionaire.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class WarningScoreStrategyTests
    {
        [TestMethod()]
        public void GetScore_NullWarnings()
        {
            // Arrange
            var target = new WarningScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(0, act);
        }

        [TestMethod()]
        public void GetScore_NonEmpryWarnings()
        {
            // Arrange
            var target = new WarningScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded, Warnings = new List<Warning> { new Warning { Total = 100 } } },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(200, act);
        }

        [TestMethod()]
        public void GetScore_EmpryWarnings()
        {
            // Arrange
            var target = new WarningScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded, Warnings = new List<Warning>() },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(0, act);
        }

    }
}