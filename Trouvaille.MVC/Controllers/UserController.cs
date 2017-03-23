using Bytes2you.Validation;
using System.Collections.Generic;
using System.Web.Mvc;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Server.Models.Users;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IUserService userService;
        private readonly IArticleService articleService;
        private readonly IPictureService pictureService;

        public UserController(
            IMappingService mappingService,
            IUserService userService,
            IArticleService articleService,
            IPictureService pictureService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();
            Guard.WhenArgument(pictureService, "pictureService").IsNull().Throw();

            this.mappingService = mappingService;
            this.userService = userService;
            this.articleService = articleService;
            this.pictureService = pictureService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ById(string id)
        {
            var user = this.userService.GetUserById(id);

            var articlesFromDb = this.articleService.GetArticlesByUserId(id);
            var picturesFromDb = this.pictureService.GetPicturesByUserId(id);

            var mappedArticles = this.mappingService.Map<IEnumerable<ArticleByIdViewModel>>(articlesFromDb);
            var mappedPictures = this.mappingService.Map<IEnumerable<PictureViewModel>>(picturesFromDb);

            var model = this.mappingService.Map<UserProfileViewModel>(user);
            model.Articles = mappedArticles;
            model.Pictures = mappedPictures;

            return View(model);
        }
    }
}