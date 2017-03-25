using Moq;
using NUnit.Framework;
using System;
using Trouvaille.MVC.Areas.Admin.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenMappingServiceIsNull()
        {
            // Arrange
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(null,
                mockedUserService.Object,
                mockedArticlesService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIUserServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(mockedMappingService.Object,
                null,
                mockedArticlesService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenArticleServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(mockedMappingService.Object,
                mockedUserService.Object,
                null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            // Act 
            var controller = new PanelController(
                mockedMappingService.Object,
                mockedUserService.Object,
                mockedArticlesService.Object);

            // Assert
            Assert.IsInstanceOf<PanelController>(controller);
        }
    }
}
