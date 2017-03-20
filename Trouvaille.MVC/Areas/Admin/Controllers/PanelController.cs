using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trouvaille.Server.Models;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IUserService userService;

        public PanelController(IMappingService mappingService, IUserService userService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.mappingService = mappingService;
            this.userService = userService;
        }

        // GET: Admin/Panel
        public ActionResult Index()
        {
            var users = this.userService.GetAllUsers();

            var mappedUsers = this.mappingService.Map<IEnumerable<UserViewModel>>(users);

            return View(mappedUsers);
        }
    }
}