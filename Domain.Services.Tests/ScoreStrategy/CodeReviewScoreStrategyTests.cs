using Farfetch.Buildionaire.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class CodeReviewScoreStrategyTests
    {
        [TestMethod()]
        public void GetScore_NullCodeReview()
        {
            // Arrange
            var target = new CodeReviewScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded },
                new Metric { Code = "metricCode", Weight = 2 });

            // Assert
            Assert.AreEqual(0, act);
        }

        [TestMethod()]
        public void GetScore_NonEmptyCodeReviewScore()
        {
            // Arrange
            var target = new CodeReviewScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded, CodeReviews = new List<CodeReview> { new CodeReview() } },
                new Metric { Code = "metricCode", Weight = 500 });

            // Assert
            Assert.AreEqual(500, act);
        }

        [TestMethod()]
        public void GetScore_EmptyCodeReviewScore()
        {
            // Arrange
            var target = new CodeReviewScoreStrategy();

            // Act
            var act = target.GetScore(
                new Build { BuildNumber = "test", Status = BuildStatus.Succeeded, CodeReviews = new List<CodeReview>() },
                new Metric { Code = "metricCode", Weight = 500 });

            // Assert
            Assert.AreEqual(0, act);
        }
    }
}