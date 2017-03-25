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

namespace Trouvaille.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class DeleteUser_Should
    {
        [Test]
        public void CallRepositoryMethodDeleteOnce_WhenUserIdIsValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string userId = "some-id";
            var mockedUser = new Mock<User>();

            mockedRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(mockedUser.Object);
            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act
            service.DeleteUser(userId);

            // Assert
            mockedRepository.Verify(x => x.Delete(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkMethodCommitOnce_WhenUserIdIsValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string userId = "some-id";
            var mockedUser = new Mock<User>();

            mockedRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(mockedUser.Object);
            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act
            service.DeleteUser(userId);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUserIdIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.DeleteUser(null));

            // Assert
            StringAssert.IsMatch("userId", message.ParamName);
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserWithIdIsNotFound()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string userId = "some-id";

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.DeleteUser(userId));

            // Assert
            StringAssert.IsMatch("User cannot be found", message.ParamName);
        }
    }
}
