using Bytes2you.Validation;
using System.Collections.Generic;
using System.Web.Mvc;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Search;
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
        private readonly IUserService userService;

        public SearchController(
            IMappingService mappingService,
            IPlaceService placeService,
            IArticleService articleService,
            IPictureService pictureService,
            IUserService userService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(placeService, "placeService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();
            Guard.WhenArgument(pictureService, "pictureService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.mappingService = mappingService;
            this.placeService = placeService;
            this.articleService = articleService;
            this.pictureService = pictureService;
            this.userService = userService;
        }

        public ActionResult Search()
        {
            var searchModel = new GeneralSearchViewModel();

            return this.PartialView("_GenericSearch", searchModel);
        }

        public ActionResult SearchBy(string text)
        {
            var articles = this.articleService.GetArticlesByTitle(text);
            var users = this.userService.GetAllUsersByUsername(text);
            var pictures = this.pictureService.GetPictureByDescription(text);

            var mappedArticles = this.mappingService.Map<IEnumerable<ArticleByIdViewModel>>(articles);
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);
            var mappedPictures = this.mappingService.Map<IEnumerable<PictureViewModel>>(pictures);

            var model = new GeneralSearchViewModel()
            {
                Text = text,
                Articles = mappedArticles,
                Pictures = mappedPictures,
                Users = mappedUsers
            };

            return this.View(model);
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