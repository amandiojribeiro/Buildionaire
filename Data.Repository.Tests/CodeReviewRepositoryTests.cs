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
    public class CodeReviewRepositoryTests
    {
        #region GetLastCodeReview
        [TestMethod]
        public void GetLast_WithNoCodeReviews_ReturnsNull()
        {
            // Arrange
            var data = new List<CodeReview>().AsQueryable();

            var mockContext = BuildCodeReviewContext(data);

            var target = new CodeReviewRepository(mockContext.Object);

            /// Act
            var act = target.GetLast();

            // Assert
            Assert.IsNull(act);
        }

        [TestMethod]
        public void GetLast_WithCodeREviews_ReturnsLastBuild()
        {
            // Arrange
            var data = new List<CodeReview>
                           {
                               new CodeReview { Id = 1, CreatedAt = new DateTime(2014, 01, 01) },
                               new CodeReview { Id = 2, CreatedAt = new DateTime(2014, 01, 02) }
                           }
                .AsQueryable();

            var mockContext = BuildCodeReviewContext(data);

            var target = new CodeReviewRepository(mockContext.Object);

            /// Act
            var act = target.GetLast();

            // Assert
            Assert.AreEqual(2, act.Id);
        }
        #endregion


        private static Mock<BuildionaireDomainContext> BuildCodeReviewContext(IQueryable<CodeReview> data)
        {
            var mockSet = new Mock<DbSet<CodeReview>>();
            mockSet.As<IQueryable<CodeReview>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CodeReview>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CodeReview>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CodeReview>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<BuildionaireDomainContext>();
            mockContext.Setup(c => c.Set<CodeReview>()).Returns(mockSet.Object);
            return mockContext;
        }
    }
}