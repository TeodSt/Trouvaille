using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.CountryServiceTests
{
    [TestFixture]
    public class GetAllCountriesOrderedByName_Should
    {
        [Test]
        public void GetAllCountriesOrdered()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();

            Country spain = new Country() { Name = "Spain" };
            Country greece = new Country() { Name = "Greece" };
            Country italy = new Country() { Name = "Italy" };

            IEnumerable<Country> countries = new List<Country> { spain, greece, italy };
            mockedRepository.Setup(x => x.GetAll()).Returns(countries);

            var expectedOrdered = countries.OrderBy(x => x.Name);
            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act
            var orderedCountries = service.GetAllCountriesOrderedByName();

            // Assert
            CollectionAssert.AreEqual(expectedOrdered, orderedCountries);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();

            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            service.GetAllCountriesOrderedByName();

            // Assert
            mockedRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
