using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Trouvaille.Server.Common;

namespace Trouvaille.UnitTests.FileProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnCorrectInstance_WhenArgumentsAreValid()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();
            var mockedServer = new Mock<HttpServerUtilityBase>();

            // Act 
            var provider = new FileProvider(mockedRequest.Object, mockedServer.Object);

            // Assert
            Assert.IsInstanceOf<FileProvider>(provider);
        }

        [Test]
        public void ThrowArgumentNullException_WhenHttpRequestBaseIsNull()
        {
            // Arrange
            var mockedServer = new Mock<HttpServerUtilityBase>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileProvider(null, mockedServer.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenHttpServerUtilityBaseIsNull()
        {
            // Arrange
            var mockedRequest = new Mock<HttpRequestBase>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileProvider(mockedRequest.Object, null));
        }
    }
}
