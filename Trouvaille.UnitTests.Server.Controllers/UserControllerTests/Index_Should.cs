using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Trouvaille.MVC.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.UserControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnView()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            // Act 
            var controller = new UserController(
                mockedMappingService.Object,
                mockedUserService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }
    }
}
