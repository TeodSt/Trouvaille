using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class GetUserById_Should
    {
        [Test]
        public void ReturnUserWithPassedId()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string userId = "some-id";
            User user = new User() { Id = userId };

            mockedRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(user);

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
           var actualUser = service.GetUserById(userId);

            // Assert
            Assert.AreSame(user, actualUser);
        }

        [Test]
        public void CallRepositoryMethodGetByIdOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string userId = "some-id";
            mockedRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(new User());

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            service.GetUserById(userId);

            // Assert
            mockedRepository.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedIsIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var exception = Assert.Throws<ArgumentNullException>(() => service.GetUserById(null));

            // Assert
            StringAssert.IsMatch("id", exception.ParamName);
        }
    }
}
