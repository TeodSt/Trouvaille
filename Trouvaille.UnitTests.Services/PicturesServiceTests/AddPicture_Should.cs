using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PicturesServiceTests
{
    [TestFixture]
    public class AddPicture_Should
    {
        [Test]
        public void CallRepositoryMethodAddOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedPicture = new Mock<Picture>();

            // Act 
            service.AddPicture(mockedPicture.Object);

            // Assert
            mockedRepository.Verify(x => x.Add(It.IsAny<Picture>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkMethodCommitOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedPicture = new Mock<Picture>();

            // Act 
            service.AddPicture(mockedPicture.Object);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenPlaceIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.AddPicture(null));

            // Assert
            StringAssert.IsMatch("picture", message.ParamName);
        }
    }
}
