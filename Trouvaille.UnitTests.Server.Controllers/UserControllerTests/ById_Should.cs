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
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Users;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.UserControllerTests
{
    [TestFixture]
    public class ById_Should
    {
        private Mock<IMappingService> mockedMappingService;
        private Mock<IArticleService> mockedArticlesService;
        private Mock<IPictureService> mockedPictureService;
        private Mock<IUserService> mockedUserService;

        private UserController controller;

        [SetUp]
        public void SetUp()
        {
            this.mockedMappingService = new Mock<IMappingService>();
            this.mockedArticlesService = new Mock<IArticleService>();
            this.mockedPictureService = new Mock<IPictureService>();
            this.mockedUserService = new Mock<IUserService>();

            this.controller = new UserController(
                mockedMappingService.Object,
                mockedUserService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object);
        }

        [Test]
        public void ReturnViewWithModelWithCorrectProperties()
        {
            // Arrange            
            IEnumerable<ArticleByIdViewModel> articlesViewModel = new List<ArticleByIdViewModel>() { new ArticleByIdViewModel() };
            IEnumerable<PictureViewModel> picturesViewModel = new List<PictureViewModel>() { new PictureViewModel() };

            UserProfileViewModel usersViewModel = new UserProfileViewModel();

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(usersViewModel);

            mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(articlesViewModel);
            mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(picturesViewModel);

            string id = "some-id";

            // Act & Assert
            this.controller.WithCallTo(x => x.ById(id))
                .ShouldRenderDefaultView()
                .WithModel<UserProfileViewModel>(viewModel =>
                {
                    CollectionAssert.AreEquivalent(articlesViewModel, viewModel.Articles);
                    CollectionAssert.AreEquivalent(picturesViewModel, viewModel.Pictures);
                });
        }

        [Test]
        public void GiveHttpStatusBadRequest_WhenPassedIdIsNull()
        {
            // Act & Assert
            controller.WithCallTo(x => x.ById(null))
               .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallGetArticlesByUsernameOnce()
        {
            // Arrange            
            string articleId = Guid.NewGuid().ToString();
            string userId = "some-id";

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.ById(userId);

            // Assert
            this.mockedArticlesService.Verify(x => x.GetArticlesByUserId(userId), Times.Once);
        }

        [Test]
        public void CallGetPicturesByUsernameOnce()
        {
            // Arrange
            string userId = "some-id";

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.ById(userId);

            // Assert
            this.mockedPictureService.Verify(x => x.GetPicturesByUserId(userId), Times.Once);
        }

        [Test]
        public void CallMapUserProfileViewModelOnce()
        {
            // Arrange
            string userId = "some-id";
            
            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.ById(userId);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<UserProfileViewModel>(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void CallMapArticleByIdViewModelOnce()
        {
            // Arrange
            string userId = "some-id";

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.ById(userId);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }

        [Test]
        public void CallMapPictureViewModelOnce()
        {
            // Arrange
            string userId = "some-id";

            this.mockedMappingService.Setup(x => x.Map<UserProfileViewModel>(It.IsAny<User>())).Returns(new UserProfileViewModel());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(new List<ArticleByIdViewModel>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(new List<PictureViewModel>());

            // Act
            this.controller.ById(userId);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>()), Times.Once);
        }
    }
}
