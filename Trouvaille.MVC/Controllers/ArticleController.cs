using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trouvaille.Models;
using Trouvaille.Server.Models.Articles;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IArticleService articleService;

        public ArticleController(IMappingService mappingService, IArticleService articleService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();

            this.mappingService = mappingService;
            this.articleService = articleService;
        }

        // GET: Article
        public ActionResult Index()
        {
          //  AddArticleViewModel model = new AddArticleViewModel();

            var articles = this.articleService.GetAllArticles();

            var mappedModels = this.mappingService.Map<IEnumerable<AddArticleViewModel>>(articles);

            return this.View(mappedModels);
        }

        public ActionResult ById(string id)
        {
            var articleFromDb = this.articleService.GetArticleById(id);
            var model = this.mappingService.Map<Article, AddArticleViewModel>(articleFromDb);

            return this.View(model);
        }
    }
}