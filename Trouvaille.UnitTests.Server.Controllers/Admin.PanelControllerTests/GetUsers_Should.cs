using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Admin.Controllers;
using Trouvaille.Server.Models;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class GetUsers_Should
    {
        [Test]
        public void ReturnPartialViewWithModel()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            string partialView = "_GetUsers";
            IEnumerable<User> users = new List<User>() { new User() };
            IEnumerable<UserViewModel> usersViewModel = new List<UserViewModel>() { new UserViewModel() };

            mockedUserService.Setup(x => x.GetAllUsers()).Returns(users);
            mockedMappingService.Setup(x => x.Map<IEnumerable<UserViewModel>>(It.IsAny<IEnumerable<User>>())).Returns(usersViewModel);

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.GetUsers())
                .ShouldRenderPartialView(partialView)
                .WithModel(usersViewModel);
        }

        [Test]
        public void CallGetAllUsersOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.GetUsers();

            // Assert
            mockedUserService.Verify(x => x.GetAllUsers(), Times.Once);
        }

        [Test]
        public void CallMapOnce()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.GetUsers();

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<UserViewModel>>(It.IsAny<IEnumerable<User>>()), Times.Once);
        }
    }
}
