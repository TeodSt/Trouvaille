using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class GetUserByUsername_Should
    {
        [TestFixture]
        public class GetUserById_Should
        {
            [Test]
            public void ReturnUserWithPassedUsername()
            {
                // Arrange
                var mockedUnitOfWork = new Mock<IUnitOfWork>();
                var mockedRepository = new Mock<IEfGenericRepository<User>>();
                string username = "username";
                User user = new User() { UserName = username };

                IEnumerable<User> users = new List<User>() { user };
                mockedRepository.Setup(x => x.All).Returns(users.AsQueryable);

                var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

                // Act 
                var actualUser = service.GetUserByUsername(username);

                // Assert
                Assert.AreSame(user, actualUser);
            }

            [Test]
            public void CallRepositoryMethodGetByIdOnce()
            {
                // Arrange
                var mockedUnitOfWork = new Mock<IUnitOfWork>();
                var mockedRepository = new Mock<IEfGenericRepository<User>>();
                string username = "username";
                mockedRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(new User());

                var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

                // Act 
                service.GetUserByUsername(username);

                // Assert
                mockedRepository.Verify(x => x.All, Times.Once);
            }

            [Test]
            public void ThrowArgumentNullException_WhenPassedIsIsNull()
            {
                // Arrange
                var mockedUnitOfWork = new Mock<IUnitOfWork>();
                var mockedRepository = new Mock<IEfGenericRepository<User>>();

                var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

                // Act 
                var exception = Assert.Throws<ArgumentNullException>(() => service.GetUserByUsername(null));

                // Assert
                StringAssert.IsMatch("username", exception.ParamName);
            }
        }
    }
}
