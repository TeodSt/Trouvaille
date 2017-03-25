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
    public class GetArticleById_Should
    {

        //TODO: Test it correctly!

       // [Test]
        //public void ReturnCorrectArticle_WhenIdIsValid()
        //{
        //    // Arrange
        //    var mockedUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockedRepository = new Mock<IEfGenericRepository<Article>>();

        //    Article expectedArticle = new Article();

        //    mockedRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(expectedArticle);

        //    var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

        //    // Act 
        //    var actualArticle = service.GetArticleById(It.IsAny<string>());

        //    // Assert
        //    Assert.AreEqual(expectedArticle, actualArticle);
        //}

        //[Test]
        //public void ReturnNull_WhenArticleWithPassedIdIsNotFound()
        //{
        //    // Arrange
        //    var mockedUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockedRepository = new Mock<IEfGenericRepository<Article>>();
        //    Article expectedArticle = new Article();

        //    string id = "id";
        //    var service = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

        //    // Act 
        //    var actualCountry = service.GetArticleById(id);

        //    // Assert
        //    Assert.IsNull(actualCountry);
        //}
    }
}
