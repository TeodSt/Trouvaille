using Moq;
using NUnit.Framework;
using System;
using Trouvaille.MVC.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.SearchControllersTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIMappingServiceIsNull()
        {
            // Arrange
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SearchController(null,
                mockedPlacesService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIPlaceServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SearchController(
                mockedMappingService.Object,
                null,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIArticleServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SearchController(mockedMappingService.Object,
                mockedPlacesService.Object,
                null,
                mockedPictureService.Object,
                mockedUserService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPictureServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SearchController(mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedArticlesService.Object,
                null,
                mockedUserService.Object));
        }
        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedPlacesService = new Mock<IPlaceService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SearchController(mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act 
            var controller = new SearchController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object);

            // Assert
            Assert.IsInstanceOf<SearchController>(controller);
        }
    }
}
