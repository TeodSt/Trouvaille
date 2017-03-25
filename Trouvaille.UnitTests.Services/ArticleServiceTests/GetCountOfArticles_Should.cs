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
    public class GetCountOfArticles_Should
    {
        [Test]
        public void ReturnCorrectCountOfArticles()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();

            IEnumerable<Article> articles = new List<Article> { new Article(), new Article(), new Article(), new Article() };
            mockedRepository.Setup(x => x.All).Returns(articles.AsQueryable);

            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualCount = articleService.GetCountOfArticles();
            var expectedCount = articles.Count();

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
