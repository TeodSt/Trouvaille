using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.ArticleControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnViewWithModelWithCorrectProperties()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();
            int page = 1;
            int maxRows = 5;
            var articles = new List<Article>() { new Article() };
            var viewModelArticles = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };

            mockedArticleService.Setup(x => x.GetAllArticles(page, maxRows)).Returns(articles);
            mockedArticleService.Setup(x => x.GetCountOfArticles()).Returns(articles.Count());

            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(articles)).Returns(viewModelArticles);

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act & Assert

            controller.WithCallTo(x => x.Index(page))
                    .ShouldRenderDefaultView()
                    .WithModel<ArticlesViewModel>(viewModel =>
                    {
                        CollectionAssert.AreEquivalent(viewModelArticles, viewModel.Articles);
                        Assert.AreEqual(page, viewModel.PageCount);
                        Assert.AreEqual(page, viewModel.CurrentPageIndex);
                    });
        }

        [Test]
        public void CallGetAllArticlesOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();
            int page = 1;
            int maxRows = 5;
            var articles = new List<Article>() { new Article() };
            var viewModelArticles = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };

            mockedArticleService.Setup(x => x.GetAllArticles(page, maxRows)).Returns(articles);
            mockedArticleService.Setup(x => x.GetCountOfArticles()).Returns(articles.Count());

            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(articles)).Returns(viewModelArticles);

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act 
            controller.Index();

            // Assert
            mockedArticleService.Verify(x => x.GetAllArticles(page, maxRows), Times.Once);
        }

        [Test]
        public void CallGetCountOfArticlesOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();
            int page = 1;
            int maxRows = 5;
            var articles = new List<Article>() { new Article() };
            var viewModelArticles = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };

            mockedArticleService.Setup(x => x.GetAllArticles(page, maxRows)).Returns(articles);
            mockedArticleService.Setup(x => x.GetCountOfArticles()).Returns(articles.Count());

            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(articles)).Returns(viewModelArticles);

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act 
            controller.Index();

            // Assert
            mockedArticleService.Verify(x => x.GetCountOfArticles(), Times.Once);
        }

        [Test]
        public void CallMapOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();
            int page = 1;
            int maxRows = 5;
            var articles = new List<Article>() { new Article() };
            var viewModelArticles = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };

            mockedArticleService.Setup(x => x.GetAllArticles(page, maxRows)).Returns(articles);
            mockedArticleService.Setup(x => x.GetCountOfArticles()).Returns(articles.Count());

            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(articles)).Returns(viewModelArticles);

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act 
            controller.Index();

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }
    }
}
