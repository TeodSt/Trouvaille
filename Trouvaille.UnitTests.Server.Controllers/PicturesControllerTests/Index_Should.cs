using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.PicturesControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnViewWithModelWithCorrectCollection()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPictureService = new Mock<IPictureService>();
            IEnumerable<Picture> pictures = new List<Picture>() { new Picture() };
            IEnumerable<PictureViewModel> viewModelPictures = new List<PictureViewModel>() { new PictureViewModel() };

            mockedPictureService.Setup(x => x.GetAllPictures()).Returns(pictures);
            mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(viewModelPictures);

            var controller = new PicturesController(mockedMappingService.Object, mockedPictureService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel(viewModelPictures);
        }

        [Test]
        public void CallGetAllPicturesOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPictureService = new Mock<IPictureService>();
            
            var controller = new PicturesController(mockedMappingService.Object, mockedPictureService.Object);

            // Act
            controller.Index();

            // Assert
            mockedPictureService.Verify(x => x.GetAllPictures(), Times.Once);
        }

        [Test]
        public void CallMapOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPictureService = new Mock<IPictureService>();

            var controller = new PicturesController(mockedMappingService.Object, mockedPictureService.Object);

            // Act
            controller.Index();

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>()), Times.Once);
        }
    }
}
