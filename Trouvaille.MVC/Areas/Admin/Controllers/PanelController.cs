using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IUserService userService;
        private readonly IArticleService articleService;

        public PanelController(IMappingService mappingService, IUserService userService, IArticleService articleService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();

            this.mappingService = mappingService;
            this.userService = userService;
            this.articleService = articleService;
        }

        // GET: Admin/Panel
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetUsers()
        {
            var users = this.userService.GetAllUsers();

            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);

            return this.PartialView("_GetUsers", mappedUsers);
        }

        public ActionResult GetArticles()
        {
            var articles = this.articleService.GetAllArticles();

            var model = this.mappingService.Map<IEnumerable<AddArticleViewModel>>(articles);

            return this.PartialView("_GetArticles", model);
        }

        [HttpPost]
        public ActionResult DeleteArticle(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //TODO: Work with ViewModel
            var article = this.articleService.GetArticleById(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            this.articleService.DeleteArticle(article);

            var articles = this.articleService.GetAllArticles();
            var model = this.mappingService.Map<IEnumerable<AddArticleViewModel>>(articles);

            return this.PartialView("_GetArticles", model);
        }

        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO: Work with ViewModel
            var user = this.userService.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            this.userService.DeleteUser(user);

            var users = this.userService.GetAllUsers();
            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);

            return this.PartialView("_GetUsers", mappedUsers);
        }
    }
}