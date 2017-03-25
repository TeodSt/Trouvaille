using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Search;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.SearchControllerTests
{
    [TestFixture]
    public class SearchBy_Should
    {
        private Mock<IMappingService> mockedMappingService;
        private Mock<IPlaceService> mockedPlacesService;
        private Mock<IArticleService> mockedArticlesService;
        private Mock<IPictureService> mockedPictureService;
        private Mock<IUserService> mockedUserService;

        private SearchController controller;

        [SetUp]
        public void SetUp()
        {
            this.mockedMappingService = new Mock<IMappingService>();
            this.mockedPlacesService = new Mock<IPlaceService>();
            this.mockedArticlesService = new Mock<IArticleService>();
            this.mockedPictureService = new Mock<IPictureService>();
            this.mockedUserService = new Mock<IUserService>();

            this.controller = new SearchController(
                 mockedMappingService.Object,
                 mockedPlacesService.Object,
                 mockedArticlesService.Object,
                 mockedPictureService.Object,
                 mockedUserService.Object);
        }

        [Test]
        public void ReturnDefaultView()
        {
            controller.WithCallTo(x => x.SearchBy("id"))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultViewWithCorrectModelProperties()
        {
            // Arrange
            string title = "title";
            string username = "username";
            string description = "description";

            IEnumerable<Article> articles = new List<Article>() { new Article() };
            IEnumerable<User> users = new List<User>() { new User() };
            IEnumerable<Picture> pictures = new List<Picture>() { new Picture() };

            IEnumerable<ArticleByIdViewModel> articlesViewModel = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };
            IEnumerable<UserViewModel> usersViewModel = new List<UserViewModel>() { new UserViewModel() };
            IEnumerable<PictureViewModel> picturesViewModel = new List<PictureViewModel>() { new PictureViewModel() };

            mockedArticlesService.Setup(x => x.GetArticlesByTitle(title)).Returns(articles);
            mockedUserService.Setup(x => x.GetAllUsersByUsername(username)).Returns(users);
            mockedPictureService.Setup(x => x.GetPictureByDescription(description)).Returns(pictures);

            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(articlesViewModel);
            mockedMappingService.Setup(x => x.Map<IEnumerable<UserViewModel>>(It.IsAny<IEnumerable<User>>())).Returns(usersViewModel);
            mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(picturesViewModel);

            // Act & Assert
            controller.WithCallTo(x => x.SearchBy("id"))
                .ShouldRenderDefaultView()
                .WithModel<GeneralSearchViewModel>(viewModel =>
                {
                    CollectionAssert.AreEquivalent(articlesViewModel, viewModel.Articles);
                    CollectionAssert.AreEquivalent(usersViewModel, viewModel.Users);
                    CollectionAssert.AreEquivalent(picturesViewModel, viewModel.Pictures);
                });
        }

        [Test]
        public void CallGetArticlesByTitleOnce()
        {
            // Arrange
            string search = "title";

            // Act
            this.controller.SearchBy(search);

            // Assert
            this.mockedArticlesService.Verify(x => x.GetArticlesByTitle(search), Times.Once);
        }

        [Test]
        public void CallGetAllUsersByUsernameOnce()
        {
            // Arrange
            string search = "title";

            // Act
            this.controller.SearchBy(search);

            // Assert
            this.mockedUserService.Verify(x => x.GetAllUsersByUsername(search), Times.Once);
        }

        [Test]
        public void CallGetPictureByDescriptionOnce()
        {
            // Arrange
            string search = "title";

            // Act
            this.controller.SearchBy(search);

            // Assert
            this.mockedPictureService.Verify(x => x.GetPictureByDescription(search), Times.Once);
        }

        [Test]
        public void CallMapArticleByIdViewModelOnce()
        {
            // Arrange
            string search = "title";

            // Act
            this.controller.SearchBy(search);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }

        [Test]
        public void CallMapUserViewModelModelOnce()
        {
            // Arrange
            string search = "title";

            // Act
            this.controller.SearchBy(search);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<UserViewModel>>(It.IsAny<IEnumerable<User>>()), Times.Once);
        }

        [Test]
        public void CallMapPictureViewModelModelOnce()
        {
            // Arrange
            string search = "title";

            // Act
            this.controller.SearchBy(search);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>()), Times.Once);
        }
    }
}
