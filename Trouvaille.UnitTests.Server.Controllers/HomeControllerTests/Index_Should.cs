using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using Trouvaille.MVC.Controllers;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnView()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlaceService = new Mock<IPlaceService>();

            var controller = new HomeController(mockedMappingService.Object, mockedPlaceService.Object);

            // Act & Assert

            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }
    }
}
