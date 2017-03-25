using Moq;
using NUnit.Framework;
using System;
using System.Net;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.ArticleControllerTests
{
    [TestFixture]
    public class ById_Should
    {
        [Test]
        public void ReturnViewWithModelWithCorrectProperties()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();
            var articleId = Guid.NewGuid();

            Article article = new Article()
            {
                Title = "title",
                Id = articleId,
                Subheader = "subheader",
                Content = "conteeent"
            };

            ArticleByIdViewModel viewModel = new ArticleByIdViewModel()
            {
                Title = "title",
                Id = articleId.ToString(),
                Subheader = "subheader",
                Content = "conteeent"
            };

            mockedArticleService.Setup(x => x.GetArticleById(articleId.ToString())).Returns(article);
            mockedMappingService.Setup(x => x.Map<Article, ArticleByIdViewModel>(It.IsAny<Article>())).Returns(viewModel);

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.ById(viewModel.Id))
                .ShouldRenderDefaultView()
                .WithModel<ArticleByIdViewModel>(m =>
                    {
                        Assert.AreEqual(article.Title, m.Title);
                        Assert.AreEqual(article.Subheader, m.Subheader);
                        Assert.AreEqual(article.Content, m.Content);
                    });
        }

        [Test]
        public void GiveHttpStatusBadRequest_WhenPassedIdIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.ById(null))
               .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallGetArticleByIdOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();
            var articleId = Guid.NewGuid();

            Article article = new Article()
            {
                Title = "title",
                Id = articleId,
                Subheader = "subheader",
                Content = "conteeent"
            };

            ArticleByIdViewModel viewModel = new ArticleByIdViewModel()
            {
                Title = "title",
                Id = articleId.ToString(),
                Subheader = "subheader",
                Content = "conteeent"
            };

            mockedArticleService.Setup(x => x.GetArticleById(articleId.ToString())).Returns(article);
            mockedMappingService.Setup(x => x.Map<Article, ArticleByIdViewModel>(It.IsAny<Article>())).Returns(viewModel);

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act 
            controller.ById(viewModel.Id);

            // Assert
            mockedArticleService.Verify(x => x.GetArticleById(It.IsAny<string>()), Times.Once);          
        }

        [Test]
        public void CallMapOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();
            var articleId = Guid.NewGuid();

            Article article = new Article()
            {
                Title = "title",
                Id = articleId,
                Subheader = "subheader",
                Content = "conteeent"
            };

            ArticleByIdViewModel viewModel = new ArticleByIdViewModel()
            {
                Title = "title",
                Id = articleId.ToString(),
                Subheader = "subheader",
                Content = "conteeent"
            };

            mockedArticleService.Setup(x => x.GetArticleById(articleId.ToString())).Returns(article);
            mockedMappingService.Setup(x => x.Map<Article, ArticleByIdViewModel>(It.IsAny<Article>())).Returns(viewModel);

            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Act 
            controller.ById(viewModel.Id);

            // Assert
            mockedMappingService.Verify(x => x.Map<Article,ArticleByIdViewModel>(It.IsAny<Article>()), Times.Once);
        }
    }
}
