using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.SearchControllerTests
{
    [TestFixture]
    public class GetPostsByContinent_Should
    {
        private Mock<IMappingService> mockedMappingService;
        private Mock<IPlaceService> mockedPlacesService;
        private Mock<IArticleService> mockedArticlesService;
        private Mock<IPictureService> mockedPictureService;
        private Mock<IUserService> mockedUserService;

        private SearchController controller;

        [SetUp]
        public void SetUp()
        {
            this.mockedMappingService = new Mock<IMappingService>();
            this.mockedPlacesService = new Mock<IPlaceService>();
            this.mockedArticlesService = new Mock<IArticleService>();
            this.mockedPictureService = new Mock<IPictureService>();
            this.mockedUserService = new Mock<IUserService>();

            this.controller = new SearchController(
                 mockedMappingService.Object,
                 mockedPlacesService.Object,
                 mockedArticlesService.Object,
                 mockedPictureService.Object,
                 mockedUserService.Object);
        }

        [Test]
        public void ReturnDefaultViewWithCorrectModelProperties()
        {
            // Arrange
            string continent = "europa";

            IEnumerable<Article> articles = new List<Article> { new Article() };
            IEnumerable<Picture> pictures = new List<Picture> { new Picture() };

            this.mockedArticlesService.Setup(x => x.GetArticlesByContinent(continent)).Returns(articles);
            this.mockedPictureService.Setup(x => x.GetPicturesByContinent(continent)).Returns(pictures);

            IEnumerable<ArticleByIdViewModel> articlesViewModel = new List<ArticleByIdViewModel> { new ArticleByIdViewModel() };
            IEnumerable<PictureViewModel> picturesViewModel = new List<PictureViewModel> { new PictureViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>())).Returns(articlesViewModel);
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>())).Returns(picturesViewModel);

            // Act & Assert
            this.controller.WithCallTo(x => x.GetPostsByContinent(continent))
                .ShouldRenderDefaultView()
                .WithModel<PostViewModel>(viewModel =>
                {
                    CollectionAssert.AreEquivalent(articlesViewModel, viewModel.Articles);
                    CollectionAssert.AreEquivalent(picturesViewModel, viewModel.Pictures);
                });
        }

        [Test]
        public void CallGetArticlesByContinentOnce()
        {
            // Arrange
            string continent = "europe";

            // Act
            this.controller.GetPostsByContinent(continent);

            // Assert
            this.mockedArticlesService.Verify(x => x.GetArticlesByContinent(continent), Times.Once);
        }

        [Test]
        public void CallGetPicturesByContinentOnce()
        {
            // Arrange
            string continent = "europe";

            // Act
            this.controller.GetPostsByContinent(continent);

            // Assert
            this.mockedPictureService.Verify(x => x.GetPicturesByContinent(continent), Times.Once);
        }

        [Test]
        public void CallMapArticleByIdViewModelOnce()
        {
            // Arrange
            string continent = "europe";

            // Act
            this.controller.GetPostsByContinent(continent);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<ArticleByIdViewModel>>(It.IsAny<IEnumerable<Article>>()), Times.Once);
        }

        [Test]
        public void CallMapPictureViewModelModelOnce()
        {
            // Arrange
            string continent = "europe";

            // Act
            this.controller.GetPostsByContinent(continent);

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<PictureViewModel>>(It.IsAny<IEnumerable<Picture>>()), Times.Once);
        }
    }
}
