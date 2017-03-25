using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.MVC.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIMappingServiceIsNull()
        {
            // Arrange
            var mockedPlaceService = new Mock<IPlaceService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(null, mockedPlaceService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIPlaceServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(mockedMappingService.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlaceService = new Mock<IPlaceService>();

            // Act 
            var controller = new HomeController(mockedMappingService.Object, mockedPlaceService.Object);

            // Assert
            Assert.IsInstanceOf<HomeController>(controller);
        }
    }
}
