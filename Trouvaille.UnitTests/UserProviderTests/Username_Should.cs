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
    public class Username_Should
    {
        [Test]
        public void ReturnCorrectUsername()
        {
            // Arrange
            var mockedContext = new Mock<HttpContextBase>();
            string name = "name";
            mockedContext.Setup(x => x.User.Identity.Name).Returns(name);

            // Act 
            var provider = new UserProvider(mockedContext.Object);

            // Assert
            Assert.AreEqual(name, provider.Username);
        }

        [Test]
        public void CallContextOnce()
        {
            // Arrange
            var mockedContext = new Mock<HttpContextBase>();
            string name = "name";
            mockedContext.Setup(x => x.User.Identity.Name).Returns(name);

            var provider = new UserProvider(mockedContext.Object);

            // Act 
            string username = provider.Username;

            // Assert
            mockedContext.Verify(x => x.User, Times.Once);
        }
    }
}
