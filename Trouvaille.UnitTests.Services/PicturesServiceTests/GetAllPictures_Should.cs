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

namespace Trouvaille.UnitTests.Services.PicturesServiceTests
{
    [TestFixture]
    public class GetAllPictures_Should
    {
        [Test]
        public void GetAllPictures()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            // Act 
            IEnumerable<Picture> expected = new List<Picture> { new Picture() };
            mockedRepository.Setup(x => x.GetAll()).Returns(expected);

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actual = service.GetAllPictures();

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void CallRepositoryMethodGetAllOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            service.GetAllPictures();

            // Assert
            mockedRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
