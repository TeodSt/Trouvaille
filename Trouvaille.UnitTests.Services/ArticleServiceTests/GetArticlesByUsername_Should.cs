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
    public class GetArticlesByUsername_Should
    {

        [Test]
        public void ReturnAllArticlesOfUserWithPassedUsername()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            string username = "username";
            var mockedUser = new Mock<User>();

            IEnumerable<Article> articles = new List<Article>() { new Article() { Creator = mockedUser.Object } };
            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>())).Returns(articles);

            var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);
            
            // Act 
            var actual = service.GetArticlesByUsername(username);

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
            string username = "some username";

            // Act 
            service.GetArticlesByUsername(username);

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>()), Times.Once);
        }

        [Test]
        public void ThowArgumentNullException_WhenUsernameIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.GetArticlesByUsername(null));

            // Assert
            StringAssert.IsMatch("username", message.ParamName);
        }
    }
}