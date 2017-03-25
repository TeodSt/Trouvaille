using Moq;
using NUnit.Framework;
using System;
using Trouvaille.MVC.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.PicturesControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIMappingServiceIsNull()
        {
            // Arrange
            var mockedPictureService = new Mock<IPictureService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PicturesController(null, mockedPictureService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIPictureServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PicturesController(mockedMappingService.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPictureService = new Mock<IPictureService>();

            // Act 
            var controller = new PicturesController(mockedMappingService.Object, mockedPictureService.Object);

            // Assert
            Assert.IsInstanceOf<PicturesController>(controller);
        }
    }
}
