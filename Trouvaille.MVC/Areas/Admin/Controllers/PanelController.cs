using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trouvaille.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        // GET: Admin/Panel
        public ActionResult Index()
        {
            return View();
        }
    }
}