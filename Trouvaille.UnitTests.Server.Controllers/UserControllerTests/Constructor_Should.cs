using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.UserControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act 
            var controller = new UserController(
                mockedMappingService.Object,
                mockedUserService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object);

            // Assert
            Assert.IsInstanceOf<UserController>(controller);
        }

        [Test]
        public void ThrowArgumentNullException_WhenIMappingServiceIsNull()
        {
            // Arrange
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(
                null,
                mockedUserService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenArticleServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(
                mockedMappingService.Object,
                mockedUserService.Object,
                null,
                mockedPictureService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPictureServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(
                mockedMappingService.Object,
                mockedUserService.Object,
                mockedArticlesService.Object,
                null));
        }
        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(
                mockedMappingService.Object,
                null,
                mockedArticlesService.Object,
                mockedPictureService.Object));
        }
    }
}
