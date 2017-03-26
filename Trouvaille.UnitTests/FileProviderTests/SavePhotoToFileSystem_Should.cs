using Moq;
using NUnit.Framework;
using System.Web;
using Trouvaille.Server.Common;

namespace Trouvaille.UnitTests.FileProviderTests
{
    [TestFixture]
    public class SavePhotoToFileSystem_Should
    {
        [Test]
        public void CallSaveAsOnce()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();
            var mockedServer = new Mock<HttpServerUtilityBase>();
            var mockedFileCollection = new Mock<HttpFileCollectionBase>();
            var mockedFiles = new Mock<HttpPostedFileBase>();
            int count = 5;

            mockedFiles.Setup(x => x.ContentLength).Returns(count);
            mockedRequest.Setup(x => x.Files.Count).Returns(count);
            mockedRequest.Setup(x => x.Files[0]).Returns(mockedFiles.Object);

            var provider = new FileProvider(mockedRequest.Object, mockedServer.Object);

            // Act 
            provider.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            mockedFiles.Verify(x => x.SaveAs(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void NotCallSaveAs_WhenNoFiles()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();
            var mockedServer = new Mock<HttpServerUtilityBase>();
            var mockedFileCollection = new Mock<HttpFileCollectionBase>();
            var mockedFiles = new Mock<HttpPostedFileBase>();
            int count = 5;
            
            mockedRequest.Setup(x => x.Files.Count).Returns(count);
            mockedRequest.Setup(x => x.Files[0]).Returns(mockedFiles.Object);

            var provider = new FileProvider(mockedRequest.Object, mockedServer.Object);

            // Act 
            provider.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            mockedFiles.Verify(x => x.SaveAs(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void NotCallSaveAs_WhenFileIsNull()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();
            var mockedServer = new Mock<HttpServerUtilityBase>();
            var mockedFileCollection = new Mock<HttpFileCollectionBase>();
            var mockedFiles = new Mock<HttpPostedFileBase>();
            int count = 5;

            mockedRequest.Setup(x => x.Files.Count).Returns(count);

            var provider = new FileProvider(mockedRequest.Object, mockedServer.Object);

            // Act 
            provider.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            mockedFiles.Verify(x => x.SaveAs(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void CallServerMapPathOnce()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();
            var mockedServer = new Mock<HttpServerUtilityBase>();
            var mockedFileCollection = new Mock<HttpFileCollectionBase>();
            var mockedFiles = new Mock<HttpPostedFileBase>();
            int count = 5;

            mockedFiles.Setup(x => x.ContentLength).Returns(count);
            mockedRequest.Setup(x => x.Files.Count).Returns(count);
            mockedRequest.Setup(x => x.Files[0]).Returns(mockedFiles.Object);

            var provider = new FileProvider(mockedRequest.Object, mockedServer.Object);

            // Act 
            provider.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            mockedServer.Verify(x => x.MapPath(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void NotCallServerMapPathOnce_WhenNoFiles()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();
            var mockedServer = new Mock<HttpServerUtilityBase>();
            var mockedFileCollection = new Mock<HttpFileCollectionBase>();
            var mockedFiles = new Mock<HttpPostedFileBase>();
            int count = 5;
            
            mockedRequest.Setup(x => x.Files.Count).Returns(count);
            mockedRequest.Setup(x => x.Files[0]).Returns(mockedFiles.Object);

            var provider = new FileProvider(mockedRequest.Object, mockedServer.Object);

            // Act 
            provider.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            mockedServer.Verify(x => x.MapPath(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void NotCallServerMapPathOnce_WhenFileIsNull()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();
            var mockedServer = new Mock<HttpServerUtilityBase>();
            var mockedFileCollection = new Mock<HttpFileCollectionBase>();
            var mockedFiles = new Mock<HttpPostedFileBase>();
            int count = 5;

            mockedRequest.Setup(x => x.Files.Count).Returns(count);

            var provider = new FileProvider(mockedRequest.Object, mockedServer.Object);

            // Act 
            provider.SavePhotoToFileSystem(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            mockedServer.Verify(x => x.MapPath(It.IsAny<string>()), Times.Never);
        }
    }
}
