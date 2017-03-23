using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Trouvaille.Models;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Places;
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

        private const string ArticlesFileLocation = "/Photos/Articles/";
        private const string PicturesFileLocation = "/Photos/Pictures/";
        private const string CountriesCache = "Countries";

        public ProfileController(
            IMappingService mappingService,
            IPlaceService placesService,
            ICountryService countryService,
            IArticleService articleService,
            IPictureService pictureService,
            IUserService userService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(placesService, "placesService").IsNull().Throw();
            Guard.WhenArgument(countryService, "countryService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();
            Guard.WhenArgument(pictureService, "pictureService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.mappingService = mappingService;
            this.placesService = placesService;
            this.countryService = countryService;
            this.articleService = articleService;
            this.pictureService = pictureService;
            this.userService = userService;
        }

        // GET: Private/Profile
        public ActionResult Index()
        {
            var user = this.userService.GetUserById(this.User.Identity.GetUserId());

            var mapped = this.mappingService.Map<UserViewModel>(user);

            return this.View(mapped);
        }

        public ActionResult Place()
        {
            AddPlaceViewModel model = new AddPlaceViewModel();
            model.Countries = this.GetAllCountries();

            return this.PartialView("_CreatePlace", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlace(AddPlaceViewModel model)
        {
            string userId = this.User.Identity.GetUserId();
            var user = this.userService.GetUserById(userId);

            model.FounderId = userId;
            model.FounderName = this.User.Identity.GetUserName();

            if (!this.ModelState.IsValid)
            {
                model.Countries = this.GetAllCountries();
                return this.PartialView("_CreatePlace", model);

            }

            var place = this.mappingService.Map<AddPlaceViewModel, Place>(model);
            place.Founder = user;
            place.Country = this.countryService.GetCountryById(model.CountryId);

            this.placesService.AddPlace(place);
            var userModel = this.mappingService.Map<UserViewModel>(user);

            return Json(new { url = Url.Action("Index", new { controller = "Places", area = "" }) });
        }

        public ActionResult CreateArticle()
        {
            AddArticleViewModel model = new AddArticleViewModel();
            model.Countries = this.GetAllCountries();

            return this.PartialView("_CreateArticle", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(AddArticleViewModel model)
        {
            string filePath = ArticlesFileLocation + model.Title.Replace(' ', '-');
            string userId = this.User.Identity.GetUserId();
            var user = this.userService.GetUserById(userId);

            model.CreatorId = userId;
            model.CreatorUsername = this.User.Identity.GetUserName();
            model.CreatedOn = DateTime.Now;
            model.ImagePath = this.SavePhotoToFileSystem(filePath);

            if (!this.ModelState.IsValid)
            {
                model.Countries = this.GetAllCountries();
                return this.PartialView("_CreateArticle", model);
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

            return PartialView("_UploadPicture", model);
        }


        [HttpPost]
        public ActionResult UploadPicture(AddPictureViewModel model)
        {
            string currentUserUsername = this.User.Identity.GetUserName();
            string userId = this.User.Identity.GetUserId();
            var user = this.userService.GetUserById(userId);

            model.CreatorId = userId;
            model.CreatorUsername = currentUserUsername;
            model.CreatedOn = DateTime.Now;

            string filePath = PicturesFileLocation + currentUserUsername;

            model.ImagePath = this.SavePhotoToFileSystem(filePath);

            var picture = this.mappingService.Map<AddPictureViewModel, Picture>(model);
            picture.Creator = user;

            this.pictureService.AddPicture(picture);

            var userModel = this.mappingService.Map<UserViewModel>(user);

            return this.View("Index", userModel);
        }

        private IEnumerable<CountryViewModel> GetAllCountries()
        {

            if (this.HttpContext.Cache[CountriesCache] == null)
            {
                var dependency = new SqlCacheDependency("Trouvaille", "Countries");

                this.HttpContext.Cache.Insert(
                    CountriesCache,                                     // key
                    this.countryService.GetAllCountriesOrderedByName(), // value
                    dependency,                                         // dependencies
                    DateTime.Now.AddDays(5),                            // absolute exp.
                    TimeSpan.Zero,                                      // sliding exp.
                    CacheItemPriority.Default,                          // priority
                    null);                                              // callback delegate            
            }

            var dbCountries = this.HttpContext.Cache[CountriesCache];
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