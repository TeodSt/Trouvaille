using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.ArticleServiceTests
{
    [TestFixture]
    public class AddArticle_Should
    {
        [Test]
        public void CallRepositoryMethodAddOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedArticle = new Mock<Article>();

            // Act 
            articleService.AddArticle(mockedArticle.Object);

            // Assert
            mockedRepository.Verify(x => x.Add(It.IsAny<Article>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkMethodCommitOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedArticle = new Mock<Article>();

            // Act 
            articleService.AddArticle(mockedArticle.Object);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenPlaceIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => articleService.AddArticle(null));

            // Assert
            StringAssert.IsMatch("article", message.ParamName);
        }
    }
}
