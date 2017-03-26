using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Private.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;
using Moq.Language.Flow;
using System.Collections.Generic;
using Trouvaille.Server.Models;

namespace Trouvaille.UnitTests.Server.Controllers.Private.ProfileControllerTests
{
    [TestFixture]
    public class Post_CreateArticle_Should
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
        public void ReturnReditectToPictures_WhenModelStateIsValid()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            string path = "some=path";

            AddArticleViewModel viewModel = new AddArticleViewModel()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3
            };

            Article article = new Article()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3,
                Id = Guid.NewGuid()
            };

            string redirectPage = "/article/byid/" + article.Id;

            //this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            //this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            //this.mockedFileProvider.Setup(x => x.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>())).Returns(path);
            //this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());

            this.mockedMappingService.Setup(x => x.Map<AddArticleViewModel, Article>(viewModel)).Returns(article);

            // Act & Assert
            this.controller.WithCallTo(x => x.CreateArticle(viewModel))
                .ShouldRedirectTo(redirectPage);
        }

        [Test]
        public void ReturnViewWithCorrectProperties_WhenModelStateIsNotValid()
        {
            // Arrange
            AddArticleViewModel viewModel = new AddArticleViewModel()
            {
                Title = "title"
            };

            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>();
            this.mockedCountryService.Setup(x => x.GetAllCountriesOrderedByName()).Returns(new List<Country>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);

            this.controller.ModelState.AddModelError("test", "error");

            // Act & Assert
            this.controller.WithCallTo(x => x.CreateArticle(viewModel))
                .ShouldRenderDefaultView()
                .WithModel<AddArticleViewModel>(model =>
                {
                    CollectionAssert.AreEquivalent(countries, model.Countries);
                });
        }


        [Test]
        public void CallGetUserByIdOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";

            AddArticleViewModel viewModel = new AddArticleViewModel()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3
            };

            Article article = new Article()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3,
                Id = Guid.NewGuid()
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddArticleViewModel, Article>(viewModel)).Returns(article);

            // Act
            this.controller.CreateArticle(viewModel);

            // Assert
            this.mockedUserService.Verify(x => x.GetUserById(userId), Times.Once);
        }

        [Test]
        public void CallUserProviderUsernameOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";

            AddArticleViewModel viewModel = new AddArticleViewModel()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3
            };

            Article article = new Article()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3,
                Id = Guid.NewGuid()
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddArticleViewModel, Article>(viewModel)).Returns(article);

            // Act 
            this.controller.CreateArticle(viewModel);

            // Assert
            this.mockedUserProvider.Verify(x => x.Username, Times.Once);
        }

        [Test]
        public void CallFileProviderSavePhotoToFileSystemOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";

            AddArticleViewModel viewModel = new AddArticleViewModel()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3
            };

            Article article = new Article()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3,
                Id = Guid.NewGuid()
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddArticleViewModel, Article>(viewModel)).Returns(article);

            // Act 
            this.controller.CreateArticle(viewModel);

            // Assert
            this.mockedFileProvider.Verify(x => x.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallMappingServiceMapOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";

            AddArticleViewModel viewModel = new AddArticleViewModel()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3
            };

            Article article = new Article()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3,
                Id = Guid.NewGuid()
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddArticleViewModel, Article>(viewModel)).Returns(article);

            // Act
            this.controller.CreateArticle(viewModel);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<AddArticleViewModel, Article>(viewModel), Times.Once);
        }

        [Test]
        public void CallPlacesServiceAddPlaceOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";

            AddArticleViewModel viewModel = new AddArticleViewModel()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3
            };

            Article article = new Article()
            {
                Title = "title",
                Subheader = "Subheader",
                CountryId = 3,
                Id = Guid.NewGuid()
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddArticleViewModel, Article>(viewModel)).Returns(article);

            // Act
            this.controller.CreateArticle(viewModel);

            // Assert
            this.mockedArticlesService.Verify(x => x.AddArticle(article), Times.Once);
        }
    }
}
