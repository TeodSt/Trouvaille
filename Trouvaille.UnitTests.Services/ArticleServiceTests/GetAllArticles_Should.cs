using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.ArticleServiceTests
{
    [TestFixture]
    public class GetAllArticles_Should
    {
        [Test]
        public void GetAllArticles()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();

            IEnumerable<Article> expected = new List<Article> { new Article() };
            mockedRepository.Setup(x => x.GetAll()).Returns(expected);

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);           

            // Act 
            var actual = articleService.GetAllArticles();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void GetAllArticlesPaged_WhenParametersArePassed()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            int firstPage = 1;
            int secondPage = 2;
            int maxRows = 2;

            IEnumerable<Article> expected = new List<Article> { new Article(), new Article(), new Article(), new Article() };
            mockedRepository.Setup(x => x.GetAll()).Returns(expected);

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualFirstPage = articleService.GetAllArticles(firstPage, maxRows);
            var actualSecondPage = articleService.GetAllArticles(secondPage, maxRows);

            // Assert
            Assert.IsFalse(actualFirstPage.Intersect(actualSecondPage).Any());
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();            
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = articleService.GetAllArticles();

            // Assert
            mockedRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce_WhenGetAllHasPassedParameters()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            int firstPage = 1;
            int maxRows = 2;

            IEnumerable<Article> expected = new List<Article> { new Article(), new Article(), new Article(), new Article() };
            mockedRepository.Setup(x => x.GetAll()).Returns(expected);

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = articleService.GetAllArticles(firstPage, maxRows);

            // Assert
            mockedRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
