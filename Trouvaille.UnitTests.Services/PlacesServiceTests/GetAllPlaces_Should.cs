using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PlacesServiceTests
{
    [TestFixture]
    public class GetAllPlaces_Should
    {
        [Test]
        public void GetAllPlaces()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Place>>();
           
            IEnumerable<Place> expected = new List<Place>() { new Place() };
            mockedRepository.Setup(x => x.GetAll()).Returns(expected);

            var service = new PlaceService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = service.GetAllPlaces();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Place>>();

            var service = new PlaceService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            service.GetAllPlaces();

            // Assert
            mockedRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
