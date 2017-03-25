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
    public class GetPicturesByUserId_Should
    {
        [Test]
        public void ReturnAllPicturesOfUserWithPassedId()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            string userId = Guid.NewGuid().ToString();
            var mockedUser = new Mock<User>();

            IEnumerable<Picture> pictures = new List<Picture>() { new Picture() { Creator = mockedUser.Object } };
            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Picture, bool>>>())).Returns(pictures);

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetPicturesByUserId(userId);

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
            string userId = Guid.NewGuid().ToString();

            // Act 
            var actualPictures = service.GetPicturesByUserId(userId);

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<Picture, bool>>>()), Times.Once);
        }

        [Test]
        public void ThowArgumentNullException_WhenUserIdIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.GetPicturesByUserId(null));

            // Assert
            StringAssert.IsMatch("userId", message.ParamName);
        }
    }
}
