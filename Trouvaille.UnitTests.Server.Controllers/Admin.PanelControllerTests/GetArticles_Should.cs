using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Admin.Controllers;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class GetArticles_Should
    {
        [Test]
        public void ReturnPartialViewWithModel()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            string partialView = "_GetArticles";
            IEnumerable<Article> articles = new List<Article>() { new Article() };
            IEnumerable<ArticleByIdViewModel> articlesViewModel = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };

            mockedArticlesService.Setup(x => x.GetAllArticles()).Returns(articles);
            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(articlesViewModel);

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.GetArticles())
                .ShouldRenderPartialView(partialView)
                .WithModel(articlesViewModel);
        }

        [Test]
        public void CallGetAllArticlesOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.GetArticles();

            // Assert
            mockedArticlesService.Verify(x => x.GetAllArticles(), Times.Once);
        }

        [Test]
        public void CallMapOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.GetArticles();

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }
    }
}
