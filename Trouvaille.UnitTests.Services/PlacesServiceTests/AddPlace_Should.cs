using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PlacesServiceTests
{
    [TestFixture]
    public class AddPlace_Should
    {
        [Test]
        public void CallRepositoryMethodAddOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Place>>();

            var service = new PlaceService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedPlace = new Mock<Place>();

            // Act 
            service.AddPlace(mockedPlace.Object);

            // Assert
            mockedRepository.Verify(x => x.Add(It.IsAny<Place>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkMethodCommitOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Place>>();

            var service = new PlaceService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedPlace = new Mock<Place>();

            // Act 
            service.AddPlace(mockedPlace.Object);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenPlaceIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Place>>();

            var service = new PlaceService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.AddPlace(null));

            // Assert
            StringAssert.IsMatch("place", message.ParamName);
        }
    }
}
