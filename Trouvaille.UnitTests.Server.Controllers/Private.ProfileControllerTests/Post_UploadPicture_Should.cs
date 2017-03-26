using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Private.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Private.ProfileControllerTests
{
    [TestFixture]
    public class Post_UploadPicture_Should
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
            AddPictureViewModel viewModel = new AddPictureViewModel()
            {
                Description = "description"
            };

            Picture picture = new Picture()
            {
                Description = "description"
            };

            string redirectPage = "/pictures";

            //this.mockedUserProvider.Setup(x => x.Username).Returns(username);
            //this.mockedUserProvider.Setup(x => x.UserId).Returns(userId);
            //this.mockedFileProvider.Setup(x => x.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>())).Returns(path);
            //this.mockedUserService.Setup(x => x.GetUserByUsername(username)).Returns(new User());

            this.mockedMappingService.Setup(x => x.Map<AddPictureViewModel, Picture>(viewModel)).Returns(picture);

            // Act & Assert
            this.controller.WithCallTo(x => x.UploadPicture(viewModel))
                .ShouldRedirectTo(redirectPage);
        }

        [Test]
        public void ReturnViewWithCorrectProperties_WhenModelStateIsNotValid()
        {
            // Arrange
            AddPictureViewModel viewModel = new AddPictureViewModel()
            {
                Description = "description"
            };

            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>();
            this.mockedCountryService.Setup(x => x.GetAllCountriesOrderedByName()).Returns(new List<Country>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);

            this.controller.ModelState.AddModelError("test", "error");

            // Act & Assert
            this.controller.WithCallTo(x => x.UploadPicture(viewModel))
                .ShouldRenderDefaultView()
                .WithModel<AddPictureViewModel>(model =>
                {
                    CollectionAssert.AreEquivalent(countries, model.Countries);
                });
        }

    }
}
