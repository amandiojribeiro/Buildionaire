using Farfetch.Buildionaire.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Farfetch.Buildionaire.Domain.Services.ScoreStrategy.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class ScoreStrategyFactoryTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ScoreStrategyFactory_StrategyNotSupported()
        {
            var x = new ScoreStrategyFactory();

            x.GetScoreStrategy("XXX");
        }

        [TestMethod()]
        public void ScoreStrategyFactory_SupportedStrategies()
        {
            var x = new ScoreStrategyFactory();

            Assert.IsInstanceOfType(x.GetScoreStrategy(Metric.BuildStatus), typeof(BuildStatusScoreStrategy));
            Assert.IsInstanceOfType(x.GetScoreStrategy(Metric.CodeCoverage), typeof(CodeCoverageScoreStrategy));
            Assert.IsInstanceOfType(x.GetScoreStrategy(Metric.CodeReview), typeof(CodeReviewScoreStrategy));
            Assert.IsInstanceOfType(x.GetScoreStrategy(Metric.Error), typeof(ErrorScoreStrategy));
            Assert.IsInstanceOfType(x.GetScoreStrategy(Metric.Warning), typeof(WarningScoreStrategy));
        }
    }
}