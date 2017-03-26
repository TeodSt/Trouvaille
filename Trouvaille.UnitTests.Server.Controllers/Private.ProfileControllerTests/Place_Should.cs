using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Caching;
using TestStack.FluentMVCTesting;
using Trouvaille.Models;
using Trouvaille.MVC.Areas.Private.Controllers;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Places;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.UnitTests.Server.Controllers.Private.ProfileControllerTests
{
    [TestFixture]
    public class Place_Should
    {
        private Mock<IMappingService> mockedMappingService;
        private Mock<IPlaceService> mockedPlacesService;
        private Mock<ICountryService> mockedCountryService;
        private Mock<IArticleService> mockedArticlesService;
        private Mock<IPictureService> mockedPictureService;
        private Mock<IUserService> mockedUserService;
        private Mock<IUserProvider> mockedUserProvider;
        private Mock<ICacheProvider> mockedCacheProvider;

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

            this.controller = new ProfileController(
                mockedMappingService.Object,
                mockedPlacesService.Object,
                mockedCountryService.Object,
                mockedArticlesService.Object,
                mockedPictureService.Object,
                mockedUserService.Object,
                mockedCacheProvider.Object,
                mockedUserProvider.Object);
        }

        [Test]
        public void ReturnViewWithModelWithCorrectProperties()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);
            this.mockedCacheProvider.Setup(x => x.SqlCacheDependency(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedCacheDependency.Object);

            string partialView = "_CreatePlace";

            // Act & Assert
            this.controller.WithCallTo(x => x.Place())
                .ShouldRenderPartialView(partialView)
                .WithModel<AddPlaceViewModel>(viewModel =>
                {
                    CollectionAssert.AreEquivalent(countries, viewModel.Countries);
                });
        }

        [Test]
        public void CallGetValueOfCacheOnce()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);
            this.mockedCacheProvider.Setup(x => x.SqlCacheDependency(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedCacheDependency.Object);

            // Act 
            this.controller.Place();

            // Assert
            this.mockedCacheProvider.Verify(x => x.GetValueOfCache(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallCacheProviderSqlCacheDependencyOnce_WhenCacheIsNull()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);
            this.mockedCacheProvider.Setup(x => x.SqlCacheDependency(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedCacheDependency.Object);

            // Act 
            this.controller.Place();

            // Assert
            this.mockedCacheProvider.Verify(x => x.SqlCacheDependency(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void NotCallCacheProviderSqlCacheDependency_WhenCacheIsNotNull()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);

            this.mockedCacheProvider.Setup(x => x.GetValueOfCache(It.IsAny<string>())).Returns(countries);

            // Act 
            this.controller.Place();

            // Assert
            this.mockedCacheProvider.Verify(x => x.SqlCacheDependency(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void NotCallGetAllCountriesOrderedByName_WhenCacheIsNotNull()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);

            this.mockedCacheProvider.Setup(x => x.GetValueOfCache(It.IsAny<string>())).Returns(countries);

            // Act 
            this.controller.Place();

            // Assert
            this.mockedCountryService.Verify(x => x.GetAllCountriesOrderedByName(), Times.Never);
        }

        [Test]
        public void CallGetAllCountriesOrderedByName_WhenCacheIsNull()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedCountryService.Setup(x => x.GetAllCountriesOrderedByName()).Returns(new List<Country>());
            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);
            
            // Act 
            this.controller.Place();

            // Assert
            this.mockedCountryService.Verify(x => x.GetAllCountriesOrderedByName(), Times.Once);
        }

        [Test]
        public void NotCallCacheProviderInsertWithSqlDependency_WhenCacheIsNotNull()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);

            this.mockedCacheProvider.Setup(x => x.GetValueOfCache(It.IsAny<string>())).Returns(countries);

            // Act 
            this.controller.Place();

            // Assert
            this.mockedCacheProvider.Verify(x => x.InsertWithSqlDependency(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CacheDependency>()), Times.Never);
        }

        [Test]
        public void CallCacheProviderInsertWithSqlDependency_WhenCacheIsNull()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);

            // Act 
            this.controller.Place();

            // Assert
            this.mockedCacheProvider.Verify(x => x.InsertWithSqlDependency(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CacheDependency>()), Times.Once);
        }

        [Test]
        public void CallMapCountryViewModelOnce()
        {
            // Arrange
            var mockedCacheDependency = new Mock<CacheDependency>();
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>() { new CountryViewModel() };

            this.mockedMappingService.Setup(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>())).Returns(countries);
            this.mockedCacheProvider.Setup(x => x.GetValueOfCache(It.IsAny<string>())).Returns(null);
            // Act 
            this.controller.Place();

            // Assert
            this.mockedMappingService.Verify(x => x.Map<IEnumerable<CountryViewModel>>(It.IsAny<IEnumerable<Country>>()), Times.Once);
        }
    }
}
