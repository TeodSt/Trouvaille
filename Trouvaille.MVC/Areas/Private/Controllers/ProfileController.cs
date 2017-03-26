using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Trouvaille.Models;
using Trouvaille.Server.Common.Contracts;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Places;
using Trouvaille.Server.Models.Users;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Areas.Private.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IPlaceService placesService;
        private readonly ICountryService countryService;
        private readonly IArticleService articleService;
        private readonly IPictureService pictureService;
        private readonly IUserService userService;
        private readonly ICacheProvider cacheProvider;
        private readonly IUserProvider userProvider;

        private const string ArticlesFileLocation = "/Photos/Articles/";
        private const string PicturesFileLocation = "/Photos/Pictures/";
        private const string CountriesCache = "Countries";
        private const string DatabaseEntryName = "Trouvaille";
        private const string PlacesRedirect = "/places";

        public ProfileController(
            IMappingService mappingService,
            IPlaceService placesService,
            ICountryService countryService,
            IArticleService articleService,
            IPictureService pictureService,
            IUserService userService,
            ICacheProvider cacheProvider,
            IUserProvider userProvider)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(placesService, "placesService").IsNull().Throw();
            Guard.WhenArgument(countryService, "countryService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();
            Guard.WhenArgument(pictureService, "pictureService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(cacheProvider, "cacheProvider").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.mappingService = mappingService;
            this.placesService = placesService;
            this.countryService = countryService;
            this.articleService = articleService;
            this.pictureService = pictureService;
            this.userService = userService;
            this.cacheProvider = cacheProvider;
            this.userProvider = userProvider;
        }

        public ActionResult Index()
        {
            var username = this.userProvider.Username;

            var user = this.userService.GetUserByUsername(username);
            var articles = this.articleService.GetArticlesByUsername(username);
            var pictures = this.pictureService.GetPicturesByUsername(username);

            var model = this.mappingService.Map<UserProfileViewModel>(user);
            var mappedArticles = this.mappingService.Map<IEnumerable<ArticleByIdViewModel>>(articles);
            var mappedPictures = this.mappingService.Map<IEnumerable<PictureViewModel>>(pictures);

            model.Articles = mappedArticles;
            model.Pictures = mappedPictures;

            return this.View(model);
        }

        public ActionResult CreatePlace()
        {
            AddPlaceViewModel model = new AddPlaceViewModel();
            model.Countries = this.GetAllCountries();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlace(AddPlaceViewModel model)
        {
            string userId = this.userProvider.UserId;
            string username = this.userProvider.Username;

            model.FounderId = userId;
            model.FounderName = username;

            if (!this.ModelState.IsValid)
            {
                model.Countries = this.GetAllCountries();              
                return this.View(model);
            }

            var user = this.userService.GetUserByUsername(username);
            var place = this.mappingService.Map<AddPlaceViewModel, Place>(model);
            place.Founder = user;
            place.Country = this.countryService.GetCountryById(model.CountryId);

            this.placesService.AddPlace(place);

            return this.Redirect(PlacesRedirect);
        }

        public ActionResult CreateArticle()
        {
            AddArticleViewModel model = new AddArticleViewModel();
            model.Countries = this.GetAllCountries();

            return this.View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(AddArticleViewModel model)
        {
            string filePath = ArticlesFileLocation + model.Title.Replace(' ', '-');
            string userId = this.userProvider.UserId;
            var user = this.userService.GetUserById(userId);

            model.CreatorId = userId;
            model.CreatorUsername = this.userProvider.Username;
            model.CreatedOn = DateTime.Now;
            model.ImagePath = this.SavePhotoToFileSystem(filePath);

            if (!this.ModelState.IsValid)
            {
                model.Countries = this.GetAllCountries();
                return this.View(model);
            }

            var article = this.mappingService.Map<AddArticleViewModel, Article>(model);
            article.Creator = user;

            this.articleService.AddArticle(article);

            return this.RedirectToAction("ById", new { controller = "Article", area = "", id = article.Id });
        }

        [HttpGet]
        public ActionResult UploadPicture()
        {
            AddPictureViewModel model = new AddPictureViewModel();
            model.Countries = this.GetAllCountries();

            return this.View(model);
        }


        [HttpPost]
        public ActionResult UploadPicture(AddPictureViewModel model)
        {
            string currentUserUsername = this.userProvider.Username;
            string userId = this.userProvider.UserId;
            var user = this.userService.GetUserById(userId);
            string filePath = PicturesFileLocation + currentUserUsername;

            model.ImagePath = this.SavePhotoToFileSystem(filePath);
            model.CreatorId = userId;
            model.CreatorUsername = currentUserUsername;
            model.CreatedOn = DateTime.Now;

            if (!this.ModelState.IsValid)
            {
                model.Countries = this.GetAllCountries();
                return this.View(model);
            }

            var picture = this.mappingService.Map<AddPictureViewModel, Picture>(model);
            picture.Creator = user;

            this.pictureService.AddPicture(picture);

            var userModel = this.mappingService.Map<UserProfileViewModel>(user);

            return this.RedirectToAction("Index", new { controller = "Pictures", area = "" });
        }

        private IEnumerable<CountryViewModel> GetAllCountries()
        {
            var cacheContent = this.cacheProvider.GetValueOfCache(CountriesCache);

            if (cacheContent == null)
            {
                var dependency = this.cacheProvider.SqlCacheDependency(DatabaseEntryName, CountriesCache);
                var value = this.countryService.GetAllCountriesOrderedByName();

                this.cacheProvider.InsertWithSqlDependency(CountriesCache, value, dependency);
                cacheContent = value;
            }

            var dbCountries = cacheContent;
            var mapped = this.mappingService.Map<IEnumerable<CountryViewModel>>(dbCountries);

            return mapped;
        }

        private string SavePhotoToFileSystem(string path)
        {
            string filePath = "";

            if (this.Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    filePath = path + "-" + fileName;
                    file.SaveAs(this.Server.MapPath(filePath));
                }
            }

            return filePath;
        }
    }
}