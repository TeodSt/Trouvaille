using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class GetAllUsers_Should
    {
        [Test]
        public void GetAllUsers()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();

            IEnumerable<User> expected = new List<User>() { new User() };
            mockedRepository.Setup(x => x.GetAll()).Returns(expected);

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = service.GetAllUsers();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            service.GetAllUsers();

            // Assert
            mockedRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
