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
    public class GetPictureByDescription_Should
    {
        [Test]
        public void ReturnPictures_WhenPassedStringMatchesPartOfPicturesDescriptionWithSameCasing()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            string description = "random";

            IEnumerable<Picture> pictures = new List<Picture>() { new Picture() { Description = description + " more text" } };

            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Picture, bool>>>())).Returns(pictures);

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetPictureByDescription(description);

            // Assert
            CollectionAssert.AreEquivalent(pictures, actualPictures);
        }

        [Test]
        public void ReturnPictures_WhenPassedStringMatchesPartOfPicturesDescriptionButDifferentCasing()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            string description = "random";

            IEnumerable<Picture> pictures = new List<Picture>() { new Picture() { Description = description + " more text" } };

            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Picture, bool>>>())).Returns(pictures);

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetPictureByDescription(description.ToUpper());

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

            // Act 
            var actualPictures = service.GetPictureByDescription(It.IsAny<string>());

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<Picture, bool>>>()), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenSearchStringIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.GetPictureByDescription(null));

            // Assert
            StringAssert.IsMatch("text", message.ParamName);
        }
    }
}
