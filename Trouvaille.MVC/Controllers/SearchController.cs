using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IPlaceService placeService;
        private readonly IArticleService articleService;
        private readonly IPictureService pictureService;

        public SearchController(
            IMappingService mappingService,
            IPlaceService placeService,
            IArticleService articleService,
            IPictureService pictureService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();
            Guard.WhenArgument(pictureService, "pictureService").IsNull().Throw();

            this.mappingService = mappingService;
            this.placeService = placeService;
            this.articleService = articleService;
            this.pictureService = pictureService;
        }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchString)
        {
            ViewBag.searchText = searchString;
            return View("~/Views/Search/Index.cshtml");
        }

        public ActionResult GetPostsByContinent(string continentName)
        {
            PostViewModel model = new PostViewModel();

            

            var articles = this.articleService.GetArticlesByContinent(continentName);
            var pictures = this.pictureService.GetPicturesByContinent(continentName);

            var articlesMapped = this.mappingService.Map<IEnumerable<AddArticleViewModel>>(articles);
            var picturesMapped = this.mappingService.Map<IEnumerable<AddPictureViewModel>>(pictures);

            model.Articles = articlesMapped;
            model.Pictures = picturesMapped;

            return View(model);
        }
    }
}