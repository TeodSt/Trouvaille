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
    public class GetArticlesByContinent_Should
    {
        [Test]
        public void ReturnAllArticlesOfCountriesInPassedContinent()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            string continent = "random";
            var mockedContinent = new Mock<Continent>();
            var country = new Country() { Continent = mockedContinent.Object };

            IEnumerable<Article> articles = new List<Article>() { new Article() { Country = country } };
            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>())).Returns(articles);

            var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = service.GetArticlesByContinent(continent);

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
            string continent = "Europe";

            // Act 
            service.GetArticlesByContinent(continent);

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<Article, bool>>>()), Times.Once);
        }

        [Test]
        public void ThowArgumentNullException_WhenContinentNameIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.GetArticlesByContinent(It.IsAny<string>()));

            // Assert
            StringAssert.IsMatch("continentName", message.ParamName);
        }
    }
}
