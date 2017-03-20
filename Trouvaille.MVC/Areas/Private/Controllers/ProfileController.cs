using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web;
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
            return this.View();
        }

        public ActionResult Place()
        {
            AddPlaceViewModel model = new AddPlaceViewModel();

            var dbCountries = this.countryService.GetAllCountries().ToList();

            //  model.Countries = this.mappingService.Map<List<CountryViewModel>>(dbCountries);

            return this.PartialView("_CreatePlace");
        }

        [HttpPost]
        public ActionResult CreatePlace(AddPlaceViewModel model)
        {
            string userId = this.User.Identity.GetUserId();

            model.FounderId = userId;
            model.FounderName = this.User.Identity.GetUserName();
            model.CountryId = 3;

            var place = this.mappingService.Map<AddPlaceViewModel, Place>(model);
            place.Founder = this.userService.GetUserById(userId);

            this.placesService.AddPlace(place);

            return this.View("Index");
        }

        public ActionResult CreateArticle()
        {
            return this.PartialView("_CreateArticle");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateArticle(AddArticleViewModel model)
        {
            string filePath = ArticlesFileLocation + model.Title.Replace(' ', '-');
            string userId = this.User.Identity.GetUserId();

            model.CreatorId = userId;
            model.CreatorUsername = this.User.Identity.GetUserName();
            model.PrivacyType = "Public";
            model.CreatedOn = DateTime.Now;
            model.ImagePath = this.SavePhotoToFileSystem(filePath);

            var article = this.mappingService.Map<AddArticleViewModel, Article>(model);
            article.Creator = this.userService.GetUserById(userId);

            this.articleService.AddArticle(article);

            return this.View("Index");
        }

        [HttpGet]
        public ActionResult UploadPicture()
        {
            return PartialView("_UploadPicture");
        }


        [HttpPost]
        public ActionResult UploadPicture(AddPictureViewModel model)
        {
            string currentUserUsername = this.User.Identity.GetUserName();
            string userId = this.User.Identity.GetUserId();
            model.CreatorId = userId;
            model.CreatorUsername = currentUserUsername;
            model.PrivacyType = "Public";
            model.CreatedOn = DateTime.Now;

            string filePath = PicturesFileLocation + currentUserUsername;

            model.Path = this.SavePhotoToFileSystem(filePath);

            var picture = this.mappingService.Map<AddPictureViewModel, Picture>(model);
            picture.Creator = this.userService.GetUserById(userId);

            this.pictureService.AddPicture(picture);

            return this.View("Index");
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