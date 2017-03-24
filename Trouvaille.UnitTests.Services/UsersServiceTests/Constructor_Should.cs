using Moq;
using NUnit.Framework;
using System;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenUserRepositoryIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null, mockedUnitOfWork.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var mockedRepository = new Mock<IEfGenericRepository<User>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(mockedRepository.Object, null));
        }


        [Test]
        public void ReturnInstance_WhenArgumentsAreValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();

            // Act 
            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Assert
            Assert.IsInstanceOf<UserService>(service);
        }
    }
}