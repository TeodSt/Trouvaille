using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.UsersServiceTests
{
    [TestFixture]
    public class GetAllUsersByUsername_Should
    {
        [Test]
        public void ReturnPictures_WhenPassedStringMatchesPartOfPicturesDescriptionWithSameCasing()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string username = "username";

            IEnumerable<User> users = new List<User>() { new User { UserName = username }, new User { UserName = username + username } };

            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<User, bool>>>())).Returns(users);

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetAllUsersByUsername(username);

            // Assert
            CollectionAssert.AreEquivalent(users, actualPictures);
        }

        [Test]
        public void ReturnPictures_WhenPassedStringMatchesPartOfPicturesDescriptionButDifferentCasing()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string username = "username";

            IEnumerable<User> users = new List<User>() { new User { UserName = username }, new User { UserName = username + username } };

            mockedRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<User, bool>>>())).Returns(users);

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetAllUsersByUsername(username.ToUpper());

            // Assert
            CollectionAssert.AreEquivalent(users, actualPictures);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            string username = "username";

            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetAllUsersByUsername(username);

            // Assert
            mockedRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenSearchStringIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<User>>();
            var service = new UserService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var message = Assert.Throws<ArgumentNullException>(() => service.GetAllUsersByUsername(null));

            // Assert
            StringAssert.IsMatch("username", message.ParamName);
        }
    }
}
