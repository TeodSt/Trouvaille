using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetArticlesByUserId_Should
    {
        [Test]
        public void ReturnAllArticlesOfUserWithPassedId()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            string userId = Guid.NewGuid().ToString();
            var mockedUser = new Mock<User>();

            IEnumerable<Article> articles = new List<Article>() { new Article() { Creator = mockedUser.Object } };
            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>())).Returns(articles);

            var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = service.GetArticlesByUserId(userId);

            // Assert
            CollectionAssert.AreEquivalent(articles, actual);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);
            string userId = Guid.NewGuid().ToString();

            // Act 
            service.GetArticlesByUserId(userId);

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>()), Times.Once);
        }

        [Test]
        public void ThowArgumentNullException_WhenUserIdIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.GetArticlesByUserId(null));

            // Assert
            StringAssert.IsMatch("userId", message.ParamName);
        }
    }
}
