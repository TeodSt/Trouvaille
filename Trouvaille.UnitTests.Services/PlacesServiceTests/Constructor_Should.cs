using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PlacesServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPlaceRepositoryIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PlaceService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedRepository = new Mock<IEfGenericRepository<Place>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PlaceService(mockedRepository.Object, null));
        }

        [Test]
        public void ReturnInstance_WhenArgumentsAreValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Place>>();

            // Act 
            var service = new PlaceService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Assert
            Assert.IsInstanceOf<PlaceService>(service);
        }
    }
}