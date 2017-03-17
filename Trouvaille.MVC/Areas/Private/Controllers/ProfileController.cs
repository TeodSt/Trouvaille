using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trouvaille.MVC.Areas.Private.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Private/Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}