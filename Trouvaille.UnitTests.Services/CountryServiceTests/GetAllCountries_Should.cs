using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.CountryServiceTests
{
    [TestFixture]
    public class GetAllCountries_Should
    {
        [Test]
        public void GetAllCountries()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();
            
            IEnumerable<Country> expected = new List<Country> { new Country() };
            mockedRepository.Setup(x => x.GetAll()).Returns(expected);

            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = service.GetAllCountries();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Country>>();
            
            var service = new CountryService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            service.GetAllCountries();

            // Assert
            mockedRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
