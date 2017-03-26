using Moq;
using NUnit.Framework;
using Trouvaille.MVC.Areas.Private.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Private.ProfileControllerTests
{
    [TestFixture]
    public class Post_CreateArticle_Should
    {

        private Mock<IMappingService> mockedMappingService;
        private Mock<IPlaceService> mockedPlacesService;
        private Mock<ICountryService> mockedCountryService;
        private Mock<IArticleService> mockedArticlesService;
        private Mock<IPictureService> mockedPictureService;
        private Mock<IUserService> mockedUserService;
        private Mock<IUserProvider> mockedUserProvider;
        private Mock<ICacheProvider> mockedCacheProvider;
        private Mock<IFileProvider> mockedFileProvider;
        private ProfileController controller;

        [SetUp]
        public void SetUp()
        {
            this.mockedMappingService = new Mock<IMappingService>();
            this.mockedPlacesService = new Mock<IPlaceService>();
            this.mockedArticlesService = new Mock<IArticleService>();
            this.mockedPictureService = new Mock<IPictureService>();
            this.mockedUserService = new Mock<IUserService>();
            this.mockedCountryService = new Mock<ICountryService>();
            this.mockedCacheProvider = new Mock<ICacheProvider>();
            this.mockedUserProvider = new Mock<IUserProvider>();
            this.mockedFileProvider = new Mock<IFileProvider>();

            this.controller = new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                mockedUserProvider.Object,
                mockedFileProvider.Object);
        }              
    }
}
