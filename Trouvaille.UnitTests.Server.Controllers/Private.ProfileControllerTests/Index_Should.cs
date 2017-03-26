using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Private.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Users;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Private.ProfileControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        private Mock<IMappingService> mockedMappingService;
        private Mock<IPlaceService> mockedPlacesService;
        private Mock<ICountryService> mockedCountryService;
        private Mock<IArticleService> mockedArticlesService;
        private Mock<IPictureService> mockedPictureService;
        private Mock<IUserService> mockedUserService;
        private Mock<IUserProvider> mockedUserProvider;
        private Mock<ICacheProvider> mockedCacheProvider;
        private Mock<IFileProvider> mockedFileProvider;

        private ProfileController controller;

        [SetUp]
        public void SetUp()
        {
            this.mockedMappingService = new Mock<IMappingService>();
            this.mockedPlacesService = new Mock<IPlaceService>();
            this.mockedArticlesService = new Mock<IArticleService>();
            this.mockedPictureService = new Mock<IPictureService>();
            this.mockedUserService = new Mock<IUserService>();
            this.mockedCountryService = new Mock<ICountryService>();
            this.mockedCacheProvider = new Mock<ICacheProvider>();
            this.mockedUserProvider = new Mock<IUserProvider>();
            this.mockedFileProvider = new Mock<IFileProvider>();

            this.controller = new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                mockedUserProvider.Object,
                mockedFileProvider.Object);
        }

        [Test]
        public void ReturnViewWithModelWithCorrectProperties()
        {
            // Arrange            
            IEnumerable<ArticleByIdViewModel> articlesViewModel = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };
            IEnumerable<PictureViewModel> picturesViewModel = new List<PictureViewModel>() { new PictureViewModel() };

            UserProfileViewModel usersViewModel = new UserProfileViewModel();

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(usersViewModel);

            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(articlesViewModel);
            mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(picturesViewModel);

            // Act & Assert
            this.controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel<UserProfileViewModel>(viewModel =>
                {
                    CollectionAssert.AreEquivalent(articlesViewModel, viewModel.Articles);
                    CollectionAssert.AreEquivalent(picturesViewModel, viewModel.Pictures);
                });
        }

        [Test]
        public void CallUserProviderUsernameOnce()
        {
            // Arrange
            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.Index();

            // Assert
            this.mockedUserProvider.Verify(x => x.Username, Times.Once);
        }

        [Test]
        public void CallGetUserByUsernameOnce()
        {
            // Arrange
            string username = "username";
            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.Index();

            // Assert
            this.mockedUserService.Verify(x => x.GetUserByUsername(username), Times.Once);
        }

        [Test]
        public void CallGetArticlesByUsernameOnce()
        {
            // Arrange
            string username = "username";
            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            
            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.Index();

            // Assert
            this.mockedArticlesService.Verify(x => x.GetArticlesByUsername(username), Times.Once);
        }

        [Test]
        public void CallGetPicturesByUsernameOnce()
        {
            // Arrange
            string username = "username";
            this.mockedUserProvider.Setup(x => x.Username).Returns(username);

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.Index();

            // Assert
            this.mockedPictureService.Verify(x => x.GetPicturesByUsername(username), Times.Once);
        }

        [Test]
        public void CallMapUserProfileViewModelOnce()
        {
            // Arrange
            string username = "username";
            this.mockedUserProvider.Setup(x => x.Username).Returns(username);

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.Index();

            // Assert
            this.mockedMappingService.Verify(x => x.Map<UserProfileViewModel>(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void CallMapArticleByIdViewModelOnce()
        {
            // Arrange
            string username = "username";
            this.mockedUserProvider.Setup(x => x.Username).Returns(username);

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.Index();

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }

        [Test]
        public void CallMapPictureViewModelOnce()
        {
            // Arrange
            string username = "username";
            this.mockedUserProvider.Setup(x => x.Username).Returns(username);

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.Index();

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>()), Times.Once);
        }        
    }
}
