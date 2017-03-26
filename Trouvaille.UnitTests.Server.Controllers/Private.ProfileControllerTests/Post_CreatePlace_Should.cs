using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Private.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models.Places;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Private.ProfileControllerTests
{
    [TestFixture]
    public class Post_CreatePlace_Should
    {
        private Mock<IMappingService> mockedMappingService;
        private Mock<IPlaceService> mockedPlacesService;
        private Mock<ICountryService> mockedCountryService;
        private Mock<IArticleService> mockedArticlesService;
        private Mock<IPictureService> mockedPictureService;
        private Mock<IUserService> mockedUserService;
        private Mock<IUserProvider> mockedUserProvider;
        private Mock<ICacheProvider> mockedCacheProvider;

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

            this.controller = new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                mockedUserProvider.Object);
        }

        [Test]
        public void ReturnReditectToPlaces_WhenModelStateIsValid()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            string redirectPage = "/places";
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            Place place = new Place()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);

            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());

            this.mockedMappingService.Setup(x => x.Map<AddPlaceViewModel, Place>(viewModel)).Returns(place);

            // Act & Assert
            this.controller.WithCallTo(x => x.CreatePlace(viewModel))
                .ShouldRedirectTo(redirectPage);
        }

        [Test]
        public void ReturnDefaultView_WhenModelStateIsNotValid()
        {
            // Arrange
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address"
            };

            this.mockedCountryService.Setup(x => x.GetAllCountriesOrderedByName()).Returns(new List<Country>());
            this.controller.ModelState.AddModelError("test", "test-1");

            // Act & Assert
            this.controller.WithCallTo(x => x.CreatePlace(viewModel))
                .ShouldRenderDefaultView()
                .WithModel<AddPlaceViewModel>();
        }

        [Test]
        public void CallUserProviderUserIdOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            Place place = new Place()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddPlaceViewModel, Place>(viewModel)).Returns(place);

            // Act 
            this.controller.CreatePlace(viewModel);

            // Assert
            this.mockedUserProvider.Verify(x => x.UserId, Times.Once);
        }

        [Test]
        public void CallUserProviderUsernameOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            Place place = new Place()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddPlaceViewModel, Place>(viewModel)).Returns(place);

            // Act 
            this.controller.CreatePlace(viewModel);

            // Assert
            this.mockedUserProvider.Verify(x => x.Username, Times.Once);
        }

        [Test]
        public void CallUserUserServiceGetUserByUsernameOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            Place place = new Place()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddPlaceViewModel, Place>(viewModel)).Returns(place);

            // Act 
            this.controller.CreatePlace(viewModel);

            // Assert
            this.mockedUserService.Verify(x => x.GetUserByUsername(username), Times.Once);
        }

        [Test]
        public void CallMappingServiceMapOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            Place place = new Place()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddPlaceViewModel, Place>(viewModel)).Returns(place);

            // Act 
            this.controller.CreatePlace(viewModel);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<AddPlaceViewModel, Place>(It.IsAny<AddPlaceViewModel>()), Times.Once);
        }

        [Test]
        public void CallCountryServiceGetCountryByIdOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            Place place = new Place()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddPlaceViewModel, Place>(viewModel)).Returns(place);

            // Act 
            this.controller.CreatePlace(viewModel);

            // Assert
            this.mockedCountryService.Verify(x => x.GetCountryById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallPlacesServiceAddPlaceOnce()
        {
            // Arrange
            string username = "username";
            string userId = "some-id";
            AddPlaceViewModel viewModel = new AddPlaceViewModel()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            Place place = new Place()
            {
                Address = "address",
                Description = "description",
                CountryId = 3
            };

            this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());
            this.mockedMappingService.Setup(x => x.Map<AddPlaceViewModel, Place>(viewModel)).Returns(place);

            // Act 
            this.controller.CreatePlace(viewModel);

            // Assert
            this.mockedPlacesService.Verify(x => x.AddPlace(It.IsAny<Place>()), Times.Once);
        }
    }
}
