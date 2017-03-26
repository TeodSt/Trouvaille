using Moq;
using NUnit.Framework;
using System;
using Trouvaille.MVC.Areas.Private.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Private.ProfileControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act 
            var controller = new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                userProvider.Object,
                mockedFileProvider.Object);

            // Assert
            Assert.IsInstanceOf<ProfileController>(controller);
        }

        [Test]
        public void ThrowArgumentNullException_WhenIMappingServiceIsNull()
        {
            // Arrange
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                null,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                userProvider.Object,
                 mockedFileProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIPlaceServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                mockedMappingService.Object,
                null,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                userProvider.Object,
                 mockedFileProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICountryServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                null,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                userProvider.Object,
                 mockedFileProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenArticleServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                null,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                userProvider.Object,
                 mockedFileProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPictureServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                null,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                userProvider.Object,
                 mockedFileProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                null,
                mockedCacheProvider.Object,
                userProvider.Object,
                 mockedFileProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenCacheProviderIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();
            var userProvider = new Mock<IUserProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                null,
                userProvider.Object,
                 mockedFileProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserProviderIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedCountryService = new Mock<ICountryService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedFileProvider = new Mock<IFileProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                null,
                mockedFileProvider.Object));
        }
    }
}
