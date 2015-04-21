using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Farfetch.Buildionaire.Domain.Model;
using System.Diagnostics.CodeAnalysis;
namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class ErrorScoreStrategyTests
    {
        [TestMethod()]
        public void GetScore_NullError()
        {
            // Arrange
            var target = new ErrorScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(0, act);
        }

        [TestMethod()]
        public void GetScore_NotNullError()
        {
            // Arrange
            var target = new ErrorScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded, Error = new Error { Total = 6 } },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(12, act);
        }

    }
}
