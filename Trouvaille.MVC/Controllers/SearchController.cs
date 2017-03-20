using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trouvaille.MVC.Controllers
{
    public class SearchController : Controller
    {
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
    }
}