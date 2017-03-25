using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using Trouvaille.MVC.Areas.Admin.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }
    }
}
