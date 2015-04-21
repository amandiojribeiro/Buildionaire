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
    public class UserRepositoryTests
    {
        [TestMethod]
        public void GetTopRankingReviewersAsync_WithTakeOne_ReturnsTopUser()
        {
            // Arrange
            var usersFakeData = GetUsersFakeData();
            var mockContext = BuildUserContext(usersFakeData);

            var target = new UserRepository(mockContext.Object);

            // Act
            var act = target.GetTopRankingReviewersAsync(1).Result;

            // Assert
            Assert.AreEqual(1, act.Count());
            Assert.AreEqual(3, act.First().Id);
        }

        [TestMethod]
        public void GetMonthTopRankingReviewersAsync_WithTakeOne_ReturnsTopUser()
        {
            // Arrange
            var usersFakeData = GetUsersMonthFakeData();
            var mockContext = BuildUserContext(usersFakeData);

            var target = new UserRepository(mockContext.Object);

            // Act
            var act = target.GetMonthTopRankingReviewersAsync(2014, 1, 1).Result;

            // Assert
            Assert.AreEqual(1, act.Count());
            Assert.AreEqual(3, act.First().Item1.Id);
        }

        [TestMethod]
        public void GetTopCheckinUserAsync_WithTakeOne_ReturnsTopUser()
        {
            // Arrange
            var usersFakeData = GetUsersFakeData();
            var mockContext = BuildUserContext(usersFakeData);

            var target = new UserRepository(mockContext.Object);

            // Act
            var act = target.GetTopCheckinUserAsync().Result;

            // Assert
            Assert.AreEqual(3, act.Id);
        }

        [TestMethod]
        public void GetMonthTopRankingReviewersAsync_WithTakeOne_ReturnsCorrectCount()
        {
            // Arrange
            var usersFakeData = GetUsersMonthFakeData();
            var mockContext = BuildUserContext(usersFakeData);

            var target = new UserRepository(mockContext.Object);

            // Act
            var act = target.GetMonthTopRankingReviewersAsync(2014, 1, 1).Result;

            // Assert
            Assert.AreEqual(1, act.Count());
            Assert.AreEqual(3, act.First().Item2);
        }

        private static IQueryable<User> GetUsersMonthFakeData()
        {
            var userList = new List<User>
                       {
                           new User 
                           { 
                                        Id = 1, 
                                        Name = "User 1", 
                                        CodeReviews = GetCodeReviewList(1, new DateTime(2014, 1, 1))
                                            .Union(GetCodeReviewList(6, new DateTime(2014, 2, 2)))
                                            .ToList() 
                           },
                            new User 
                           { 
                                        Id = 2, 
                                        Name = "User 2", 
                                        CodeReviews = GetCodeReviewList(2, new DateTime(2014, 1, 1))
                                            .Union(GetCodeReviewList(6, new DateTime(2014, 2, 2)))
                                            .ToList() 
                           },
                            new User 
                           { 
                                        Id = 3, 
                                        Name = "User 3", 
                                        CodeReviews = GetCodeReviewList(3, new DateTime(2014, 1, 1))
                                            .Union(GetCodeReviewList(1, new DateTime(2014, 2, 2)))
                                            .ToList() 
                           },
                       };

            foreach (var user in userList)
            {
                foreach (var codereview in user.CodeReviews)
                {
                    codereview.RequestedBy = user;
                }
            }

            return userList.AsQueryable();
        }

        private static IQueryable<User> GetUsersFakeData()
        {
            return new List<User>
                       {
                           new User { Id = 1, Name = "User 1", CodeReviews = GetCodeReviewList(1), ChangeSets = CreateFakeChangeSetList(1) },
                           new User { Id = 2, Name = "User 2", CodeReviews = GetCodeReviewList(2), ChangeSets = CreateFakeChangeSetList(2) },
                           new User { Id = 3, Name = "User 3", CodeReviews = GetCodeReviewList(3), ChangeSets = CreateFakeChangeSetList(3) }
                       }.AsQueryable();
        }

        private static ICollection<ChangeSet> CreateFakeChangeSetList(int cnt)
        {
            return Enumerable.Range(1, cnt).Select(x => new ChangeSet() { Id = x }).ToList();

        }

        private static ICollection<CodeReview> GetCodeReviewList(int cnt)
        {
            return GetCodeReviewList(cnt, new DateTime(2014, 1, 1));
        }

        private static IList<CodeReview> GetCodeReviewList(int cnt, DateTime day)
        {
            return Enumerable.Range(1, cnt).Select(x => new CodeReview { CreatedAt = day, ExternalId = x }).ToList();
        }


        private static Mock<BuildionaireDomainContext> BuildUserContext(IQueryable<User> data)
        {
            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<BuildionaireDomainContext>();

            mockContext.Setup(c => c.Set<User>()).Returns(mockSet.Object);
            return mockContext;
        }
    }
}
