using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Admin.Controllers;
using Trouvaille.Server.Models;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class DeleteUser_Should
    {
        [Test]
        public void GiveHttpStatusBadRequest_WhenPassedIdIsNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act & Arrange
            controller.WithCallTo(x => x.DeleteUser(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallDeleteArticle_WhenIdIsNotNull()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            string id = Guid.NewGuid().ToString();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.DeleteUser(id);

            // Assert
            mockedUserService.Verify(x => x.DeleteUser(id), Times.Once);
        }

        [Test]
        public void ReturnPartialViewWithModel()
        {
            // Arrange
            var mockedMappingService = new Mock<IMappingService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedUserService = new Mock<IUserService>();

            string partialView = "_GetUsers";
            string id = Guid.NewGuid().ToString();

            IEnumerable<User> users = new List<User>() { new User() };
            IEnumerable<UserViewModel> usersViewModel = new List<UserViewModel>() { new UserViewModel() };

            mockedUserService.Setup(x => x.GetAllUsers()).Returns(users);
            mockedMappingService.Setup(x => x.Map<IEnumerable<UserViewModel>>(It.IsAny<IEnumerable<User>>())).Returns(usersViewModel);

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.DeleteUser(id))
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
            string id = Guid.NewGuid().ToString();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.DeleteUser(id);

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
            string id = Guid.NewGuid().ToString();

            var controller = new PanelController(
                 mockedMappingService.Object,
                 mockedUserService.Object,
                 mockedArticlesService.Object);

            // Act
            controller.DeleteUser(id);

            // Assert
            mockedMappingService.Verify(x => x.Map<IEnumerable<UserViewModel>>(It.IsAny<IEnumerable<User>>()), Times.Once);
        }
    }
}
