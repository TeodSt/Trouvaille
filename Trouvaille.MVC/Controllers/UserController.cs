using Bytes2you.Validation;
using System.Collections.Generic;
using System.Web.Mvc;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IUserService userService;
        private readonly IArticleService articleService;

        public UserController(IMappingService mappingService, IUserService userService, IArticleService articleService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();

            this.mappingService = mappingService;
            this.userService = userService;
            this.articleService = articleService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ById(string id)
        {
            var user = this.userService.GetUserById(id);

            var articlesFromDb = this.articleService.GetArticlesByUserId(id);
            var mapped = this.mappingService.Map<IEnumerable<AddArticleViewModel>>(articlesFromDb);

            var model = this.mappingService.Map<UserViewModel>(user);
            model.Articles = mapped;

            return View(model);
        }
    }
}