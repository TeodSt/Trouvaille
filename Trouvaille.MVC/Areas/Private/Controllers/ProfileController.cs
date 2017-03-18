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
    public class ProfileController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IPlaceService placesService;
        private readonly ICountryService countryService;
        private readonly IArticleService articleService;
        private readonly IPictureService pictureService;

        public ProfileController(
            IMappingService mappingService,
            IPlaceService placesService,
            ICountryService countryService,
            IArticleService articleService,
            IPictureService pictureService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(placesService, "placesService").IsNull().Throw();
            Guard.WhenArgument(countryService, "countryService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();
            Guard.WhenArgument(pictureService, "pictureService").IsNull().Throw();

            this.mappingService = mappingService;
            this.placesService = placesService;
            this.countryService = countryService;
            this.articleService = articleService;
            this.pictureService = pictureService;
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
            model.FounderId = this.User.Identity.GetUserId();
            model.CountryId = 3;

            var place = this.mappingService.Map<AddPlaceViewModel, Place>(model);

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
            model.CreatorId = this.User.Identity.GetUserId();
            model.PrivacyType = "Public";
            model.CreatedOn = DateTime.Now;

            var article = this.mappingService.Map<AddArticleViewModel, Article>(model);

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

            model.CreatorId = this.User.Identity.GetUserId();
            model.CreatorUsername = currentUserUsername;
            model.PrivacyType = "Public";
            model.CreatedOn = DateTime.Now;

            string filePath = "";

            if (this.Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    filePath = "/Photos/Pictures/" + currentUserUsername + "-"+ fileName;
                    file.SaveAs(this.Server.MapPath(filePath));
                }
            }

            model.Path = filePath;
            var picture = this.mappingService.Map<AddPictureViewModel, Picture>(model);

            this.pictureService.AddPicture(picture);

            return this.View("Index");
        }
    }
}