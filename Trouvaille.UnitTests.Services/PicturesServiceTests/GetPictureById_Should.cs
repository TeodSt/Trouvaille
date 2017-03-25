using Moq;
using NUnit.Framework;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PicturesServiceTests
{
    [TestFixture]
    public class GetPictureById_Should
    {
        [Test]
        public void ReturnCorrectPicture_WhenIdIsValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            Picture expectedPicture = new Picture();

            mockedRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(expectedPicture);
            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var picture = service.GetPictureById(It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedPicture, picture);
        }

        [Test]
        public void ReturnNull_WhenThereIsNoPictureWithPassedId()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var picture = service.GetPictureById(It.IsAny<int>());

            // Assert
            Assert.IsNull(picture);
        }

        [Test]
        public void CallRepositoryMethodGetByIdOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            Picture expectedPicture = new Picture();

            mockedRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(expectedPicture);

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var picture = service.GetPictureById(It.IsAny<int>());

            // Assert
            mockedRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
