using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models.Places;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.PlacesControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnViewWithModelWithCorrectCollection()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();

            IEnumerable<Place> places = new List<Place>() { new Place() };
            IEnumerable<PlaceViewModel> viewModelPlaces = new List<PlaceViewModel>() { new PlaceViewModel() };

            mockedPlacesService.Setup(x => x.GetAllPlaces()).Returns(places);
            mockedMappingService.Setup(x => x.Map<IEnumerable<PlaceViewModel>>(It.IsAny<IEnumerable<Place>>())).Returns(viewModelPlaces);

            var controller = new PlacesController(mockedMappingService.Object, mockedPlacesService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel(viewModelPlaces);
        }

        [Test]
        public void CallGetAllPlacesOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();

            var controller = new PlacesController(mockedMappingService.Object, mockedPlacesService.Object);

            // Act
            controller.Index();

            // Assert
            mockedPlacesService.Verify(x => x.GetAllPlaces(), Times.Once);
        }

        [Test]
        public void CallMapOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();

            var controller = new PlacesController(mockedMappingService.Object, mockedPlacesService.Object);

            // Act
            controller.Index();

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<PlaceViewModel>>(It.IsAny<IEnumerable<Place>>()), Times.Once);
        }
    }
}
