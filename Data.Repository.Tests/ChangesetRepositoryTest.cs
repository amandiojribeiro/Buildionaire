using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.Repository.Tests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Farfetch.Buildionaire.Data.Repository;
    using Farfetch.Buildionaire.Data.Repository.DomainContext;
    using Farfetch.Buildionaire.Domain.Model;

    using Moq;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ChangesetRepositoryTest
    {
        [TestMethod]
        public void GetLastChangeset_WithNoChangesets_ReturnsNull()
        {
            // Arrange
            var data = new List<ChangeSet>().AsQueryable();

            var mockContext = BuildChangesetContext(data);

            var target = new ChangesetRepository(mockContext.Object);

            /// Act
            var act = target.GetLast();

            // Assert
            Assert.IsNull(act);
        }

        [TestMethod]
        public void GetLastChangeset_WithChangesets_ReturnsLastBuild()
        {
            // Arrange
            var data = new List<ChangeSet>
                           {
                               new ChangeSet { Id = 1, CreatedAt = new DateTime(2014, 01, 01) },
                               new ChangeSet { Id = 2, CreatedAt = new DateTime(2014, 01, 02) }
                           }
                .AsQueryable();

            var mockContext = BuildChangesetContext(data);

            var target = new ChangesetRepository(mockContext.Object);

            /// Act
            var act = target.GetLast();

            // Assert
            Assert.AreEqual(2, act.Id);
        }

        private static Mock<BuildionaireDomainContext> BuildChangesetContext(IQueryable<ChangeSet> data)
        {
            var mockSet = new Mock<DbSet<ChangeSet>>();
            mockSet.As<IQueryable<ChangeSet>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ChangeSet>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ChangeSet>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ChangeSet>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<BuildionaireDomainContext>();

            mockContext.Setup(c => c.Set<ChangeSet>()).Returns(mockSet.Object);
            return mockContext;
        }
    }
}
