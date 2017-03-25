using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class DeleteArticle_Should
    {
        [Test]
        public void GiveHttpStatusBadRequest_WhenPassedIdIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act & Arrange
            controller.WithCallTo(x => x.DeleteArticle(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallDeleteArticle_WhenIdIsNotNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            string id = Guid.NewGuid().ToString();
            
            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.DeleteArticle(id);

            // Assert
            mockedArticlesService.Verify(x => x.DeleteArticle(id), Times.Once);
        }

        [Test]
        public void ReturnPartialViewWithModel()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            string partialView = "_GetArticles";
            string id = Guid.NewGuid().ToString();
            
            IEnumerable<Article> articles = new List<Article>() { new Article() };
            IEnumerable<ArticleByIdViewModel> articlesViewModel = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };

            mockedArticlesService.Setup(x => x.GetAllArticles()).Returns(articles);
            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(articlesViewModel);

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteArticle(id))
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

            string id = Guid.NewGuid().ToString();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.DeleteArticle(id);

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
            string id = Guid.NewGuid().ToString();
            
            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);
            // Act
            controller.DeleteArticle(id);

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }
    }
}
