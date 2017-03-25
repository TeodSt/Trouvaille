using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models.Places;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.HomeControllerTests
{
    [TestFixture]
    public class GetPlaces_Should
    {
        [Test]
        public void ReturnJsonWithCorrectAddressAndDescriptionProperties()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlaceService = new Mock<IPlaceService>();

            string address = "address";
            string description = "description";

            IEnumerable<Place> places = new List<Place>() { new Place() { Address = address, Description = description } };
            IEnumerable<PlaceViewModel> viewModelPlaces = new List<PlaceViewModel>() { new PlaceViewModel() { Address = address, Description = description } };

            mockedPlaceService.Setup(x => x.GetAllPlaces()).Returns(places);
            mockedMappingService.Setup(x => x.Map<IEnumerable<PlaceViewModel>>(It.IsAny<IEnumerable<Place>>())).Returns(viewModelPlaces);

            var controller = new HomeController(mockedMappingService.Object, mockedPlaceService.Object);

            // Act & Assert
            controller.WithCallTo(c => c.GetPlaces())
                .ShouldReturnJson(data =>
            {
                Assert.That(data[0].Address, Is.EqualTo(address));
                Assert.That(data[0].Description, Is.EqualTo(description));
            });
        }

        [Test]
        public void CallGetAllPlacesOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlaceService = new Mock<IPlaceService>();
            
            var controller = new HomeController(mockedMappingService.Object, mockedPlaceService.Object);

            // Act 
            controller.GetPlaces();

            // Assert
            mockedPlaceService.Verify(x => x.GetAllPlaces(), Times.Once);           
        }

        [Test]
        public void CallMapPlacesOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlaceService = new Mock<IPlaceService>();

            var controller = new HomeController(mockedMappingService.Object, mockedPlaceService.Object);

            // Act 
            controller.GetPlaces();

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<PlaceViewModel>>(It.IsAny<IEnumerable<Place>>()), Times.Once);
        }
    }
}
