using Moq;
using NUnit.Framework;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.CountryServiceTests
{
    [TestFixture]
    public class GetCountryById_Should
    {
        [Test]
        public void ReturnCorrectCountry_WhenIdIsValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();
            
            Country country = new Country();

            mockedRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(country);

            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualCountry = service.GetCountryById(It.IsAny<int>());

            // Assert
            Assert.AreEqual(country, actualCountry);
        }

        [Test]
        public void ReturnNull_WhenCountryWithPassedIdIsNotFound()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();
            Country country = new Country();

            int id = 8;
            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualCountry = service.GetCountryById(id);

            // Assert
            Assert.IsNull(actualCountry);
        }

        [Test]
        public void CallRepositoryMethodGetByIdOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();
            Country country = new Country();
            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act
            service.GetCountryById(It.IsAny<int>());

            // Arrange
            mockedRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}