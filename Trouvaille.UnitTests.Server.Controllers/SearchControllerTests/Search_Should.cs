using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using Trouvaille.MVC.Controllers;
using Trouvaille.Server.Models.Search;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void ReturnPartialViewWithGeneralSearchViewModel()
        {
            // Act 
            var mockedMappingService = new Mock<IMappingService>();
            var mockedPlacesService = new Mock<IPlaceService>();
            var mockedArticlesService = new Mock<IArticleService>();
            var mockedPictureService = new Mock<IPictureService>();
            var mockedUserService = new Mock<IUserService>();

            string partialViewName = "_GenericSearch";

            var controller = new SearchController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object);

            // Act & Assert
            controller.WithCallTo(x => x.Search())
                .ShouldRenderPartialView(partialViewName)
                .WithModel<GeneralSearchViewModel>();
        }
    }
}
