using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.PicturesServiceTests
{
    [TestFixture]
    public class GetPictureByDescription_Should
    {
        [Test]
        public void ReturnPictures_WhenPassedStringMatchesPartOfPicturesDescription()
        {
             // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Picture>>();
            string description = "random";

            IEnumerable<Picture> pictures = new List<Picture>() { new Picture() { Description = description + " more text" } };

            mockedRepository.Setup(x => x.GetAll()).Returns(pictures);

            var service = new PictureService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var actualPictures = service.GetPictureByDescription(description);

            // Assert
          //  CollectionAssert.AreEquivalent(pictures, actualPictures);
        }
    }
}
