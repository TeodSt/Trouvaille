using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PicturesServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPictureRepositoryIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PictureService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PictureService(mockedRepository.Object, null));
        }


        [Test]
        public void ReturnInstance_WhenArgumentsAreValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            // Act 
            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Assert
            Assert.IsInstanceOf<PictureService>(service);
        }
    }
}