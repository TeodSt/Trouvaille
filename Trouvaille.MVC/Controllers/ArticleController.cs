using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Net;
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

        private const int MaxRows = 5;

        public ArticleController(IMappingService mappingService, IArticleService articleService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(articleService, "articleService").IsNull().Throw();

            this.mappingService = mappingService;
            this.articleService = articleService;
        }

        public ActionResult Index(int page = 1)
        {
            var articles = this.articleService.GetAllArticles(page, MaxRows);
            int  countOfArticles = this.articleService.GetCountOfArticles();

            var mappedModels = this.mappingService.Map<IEnumerable<ArticleByIdViewModel>>(articles);
            ArticlesViewModel model = new ArticlesViewModel();
            double pageCount = (double)(countOfArticles / Convert.ToDecimal(MaxRows));

            model.PageCount = (int)Math.Ceiling(pageCount);
            model.Articles = mappedModels;
            model.CurrentPageIndex = page;

            return this.View(model);
        }

        public ActionResult ById(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var articleFromDb = this.articleService.GetArticleById(id);
            var model = this.mappingService.Map<Article, ArticleByIdViewModel>(articleFromDb);

            return this.View(model);
        }
    }
}