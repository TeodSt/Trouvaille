using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PicturesServiceTests
{
    [TestFixture]
    public class GetPicturesByContinent_Should
    {
        [Test]
        public void ReturnAllPicturesOfCountriesInPassedContinent()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            string continent = "random";

            var mockedContinent = new Mock<Continent>();
            var country = new Country() { Continent = mockedContinent.Object };

            IEnumerable<Picture> pictures = new List<Picture>() { new Picture() { Country = country } };

            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Picture, bool>>>())).Returns(pictures);

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetPicturesByContinent(continent);

            // Assert
            CollectionAssert.AreEquivalent(pictures, actualPictures);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);
            string continent = "Europe";

            // Act 
            var actualPictures = service.GetPicturesByContinent(continent);

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<Picture, bool>>>()), Times.Once);
        }

        [Test]
        public void ThowArgumentNullException_WhenContinentNameIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.GetPicturesByContinent(It.IsAny<string>()));

            // Assert
            StringAssert.IsMatch("continentName", message.ParamName);
        }
    }
}
