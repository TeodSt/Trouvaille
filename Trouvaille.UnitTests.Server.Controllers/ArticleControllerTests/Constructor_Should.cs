using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.MVC.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.ArticleControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenMappingServiceIsNull()
        {
            // Arrange
            var mockedArticleService = new Mock<IArticleService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticleController(null, mockedArticleService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenArticleServiceIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticleController(mockedMappingService.Object, null));
        }

        [Test]
        public void ReturnCorrectInstance_WhenParametersAreValid()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticleService = new Mock<IArticleService>();

            // Act 
            var controller = new ArticleController(mockedMappingService.Object, mockedArticleService.Object);

            // Assert
            Assert.IsInstanceOf<ArticleController>(controller);
        }
    }
}
