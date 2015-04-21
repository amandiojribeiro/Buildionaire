using Farfetch.Buildionaire.Domain.Core;
using Farfetch.Buildionaire.Domain.Core.GatewayInterfaces;
using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
using Farfetch.Buildionaire.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Farfetch.Buildionaire.Application.Services.DataImporter.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class ImportCodeReviewServicesTests
    {
        [TestMethod()]
        public void ImportCodeReviewsEmptyCodeReviewsTest()
        {
            var changesetRepositoryMock = new Mock<ICodeReviewRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<ICodeReviewGateway>();
            var importCodeReviewServices = new ImportCodeReviewServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object);
            importCodeReviewServices.ImportCodeReviews();
            changesetRepositoryMock.Verify(e => e.Add(It.IsAny<CodeReview>()), Times.Never);
        }

        [TestMethod()]
        public void ImportCodeReviewsRepeatedImportTest()
        {
            var changesetRepositoryMock = new Mock<ICodeReviewRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<ICodeReviewGateway>();
            changesetGateway.Setup(e => e.GetCodeReviews(It.IsAny<DateTime>()))
                .Returns(new List<CodeReview>() { new CodeReview { ExternalId = 1 } });
            changesetRepositoryMock.Setup(e => e.Exists(It.IsAny<Expression<Func<CodeReview, bool>>>()))
                .Returns<Expression<Func<CodeReview, bool>>>(x => x.Compile().Invoke(new CodeReview { ExternalId = 1 }));
            var importCodeReviewServices = new ImportCodeReviewServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object);
            importCodeReviewServices.ImportCodeReviews();
            changesetRepositoryMock.Verify(e => e.Add(It.IsAny<CodeReview>()), Times.Never);
        }

        [TestMethod()]
        public void ImportCodeReviewsLastDateFirstCallTest()
        {
            var changesetRepositoryMock = new Mock<ICodeReviewRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<ICodeReviewGateway>();

            var importCodeReviewServices = new ImportCodeReviewServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object);
            importCodeReviewServices.ImportCodeReviews();

            changesetGateway.Verify(e => e.GetCodeReviews(It.Is<DateTime>(c => c == new DateTime(2014, 1, 1))), Times.Once);
        }

        [TestMethod()]
        public void ImportCodeReviewsLastDateTest()
        {
            var changesetRepositoryMock = new Mock<ICodeReviewRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<ICodeReviewGateway>();

            changesetRepositoryMock.Setup(e => e.GetLast()).Returns(new CodeReview { CreatedAt = new DateTime(2015, 2, 1) });

            var importCodeReviewServices = new ImportCodeReviewServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object);
            importCodeReviewServices.ImportCodeReviews();

            changesetGateway.Verify(e => e.GetCodeReviews(It.Is<DateTime>(c => c == new DateTime(2015, 2, 1))), Times.Once);
        }

        [TestMethod()]
        public void ImportCodeReviewsTest()
        {
            var changesetRepositoryMock = new Mock<ICodeReviewRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var changesetGateway = new Mock<ICodeReviewGateway>();

            changesetGateway.Setup(e => e.GetCodeReviews(It.IsAny<DateTime>()))
                .Returns(new List<CodeReview>() { new CodeReview { ExternalId = 2,  RequestedBy = new User { Name = "UserName" } } });

            changesetRepositoryMock.Setup(e => e.Exists(It.IsAny<Expression<Func<CodeReview, bool>>>()))
                .Returns<Expression<Func<CodeReview, bool>>>(x => x.Compile().Invoke(new CodeReview { ExternalId = 1 }));

            userRepositoryMock.Setup(e => e.Find(It.Is<Expression<Func<User, bool>>>(x => x.Compile().Invoke(new User { Name = "UserName" })), null))
                .Returns(new User { Name = "UserName" });

            var importCodeReviewServices = new ImportCodeReviewServices(changesetRepositoryMock.Object, userRepositoryMock.Object, changesetGateway.Object);
            importCodeReviewServices.ImportCodeReviews();
            changesetRepositoryMock.Verify(e => e.Add(It.Is<CodeReview>(c => c.RequestedBy.Name == "UserName" && c.ExternalId == 2)), Times.Once);
        }

    }
}