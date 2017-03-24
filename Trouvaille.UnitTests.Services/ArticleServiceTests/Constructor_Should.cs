using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.ArticleServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenArticleRepositoryIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticleService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticleService(mockedRepository.Object, null));
        }


        [Test]
        public void ReturnInstance_WhenArgumentsAreValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();

            // Act 
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Assert
            Assert.IsInstanceOf<ArticleService>(articleService);
        }
    }
}