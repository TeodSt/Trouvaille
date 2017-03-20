using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trouvaille.Server.Models;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IArticleService articleService;
        private readonly IPictureService pictureService;


        public HomeController(IMappingService mappingService, IArticleService articleService, IPictureService pictureService)
        {
            this.mappingService = mappingService;
            this.articleService = articleService;
            this.pictureService = pictureService;
        }

        public ActionResult Index()
        {
            var articles = this.articleService.GetAllArticles(1, 8);
            var pictures = this.pictureService.GetAllPictures();

            var articlesMapped = this.mappingService.Map<IEnumerable<PostViewModel>>(articles);
            var picturesMapped = this.mappingService.Map<IEnumerable<PostViewModel>>(pictures);

            IEnumerable<PostViewModel> model = articlesMapped.Concat(picturesMapped);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}