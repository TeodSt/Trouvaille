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

namespace Trouvaille.UnitTests.Server.Controllers.PlacesControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {

        //  public PlacesController(IMappingService mappingService, IPlaceService placeService)
        [Test]
        public void ThrowArgumentNullException_WhenIMappingServiceIsNull()
        {
            // Arrange
            var mockedPlacesService = new Mock<IPlaceService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PlacesController(null, mockedPlacesService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIPlaceServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PlacesController(mockedMappingService.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();

            // Act 
            var controller = new PlacesController(mockedMappingService.Object, mockedPlacesService.Object);

            // Assert
            Assert.IsInstanceOf<PlacesController>(controller);
        }
    }
}
