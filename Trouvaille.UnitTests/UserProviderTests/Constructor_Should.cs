using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Trouvaille.Server.Common;

namespace Trouvaille.UnitTests.UserProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenHttpContextBaseIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserProvider(null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParameterIsValid()
        {
            // Arrange
            var mockedContext = new Mock<HttpContextBase>();

            // Act 
            var provider = new UserProvider(mockedContext.Object);

            // Assert
            Assert.IsInstanceOf<UserProvider>(provider);
        }
    }
}
