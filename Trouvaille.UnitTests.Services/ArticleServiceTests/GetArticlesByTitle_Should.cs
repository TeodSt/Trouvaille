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
    public class GetArticlesByTitle_Should
    {
        [Test]
        public void ReturnArticles_WhenPassedStringMatchesPartOfArticlesTitleWithSameCasing()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            string title = "title";
            IEnumerable<Article> articles = new List<Article>() { new Article() { Title = title + "more" } };
            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>())).Returns(articles);

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = articleService.GetArticlesByTitle(title);

            // Assert
            CollectionAssert.AreEquivalent(articles, articles);
        }

        [Test]
        public void ReturnArticles_WhenPassedStringMatchesPartOfArticlesTitleButDifferentCasing()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            string title = "title";
            IEnumerable<Article> articles = new List<Article>() { new Article() { Title = title } };
            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>())).Returns(articles);

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = articleService.GetArticlesByTitle(title.ToUpper());

            // Assert
            CollectionAssert.AreEquivalent(articles, articles);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);
            string title = "title";

            // Act 
            articleService.GetArticlesByTitle(title);

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>()), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenSearchStringIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => articleService.GetArticlesByTitle(null));

            // Assert
            StringAssert.IsMatch("title", message.ParamName);
        }
    }
}
