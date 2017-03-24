using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.CountryServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenCountryRepositoryIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CountryService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CountryService(mockedRepository.Object, null));
        }


        [Test]
        public void ReturnInstance_WhenArgumentsAreValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();

            // Act 
            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Assert
            Assert.IsInstanceOf<CountryService>(service);
        }
    }
}