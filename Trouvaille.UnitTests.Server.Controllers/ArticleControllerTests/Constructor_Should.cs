using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.MVC.Controllers;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.ArticleControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        //ArticleController(IMappingService mappingService, IArticleService articleService)
        [Test]
        public void ThrowArgumentNullException_WhenMappingServiceIsNull()
        {
            // Arrange
            var mockedArticleService = new Mock<IArticleService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ArticleController(null, mockedArticleService.Object));
        }
    }
}
