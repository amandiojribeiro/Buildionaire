namespace Farfetch.Buildionaire.Domain.Services.Tests.ScoreStrategy
{
    using Farfetch.Buildionaire.Domain.Model;
    using Farfetch.Buildionaire.Domain.Services.ScoreStrategy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildStatusScoreStrategyTests
    {
        [TestMethod]
        public void GetScore_WhenCalled_ReturnsStatusMultipliedByWeight()
        {
            // Arrange
            var target = new BuildStatusScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(4, act);
        }
    }
}
