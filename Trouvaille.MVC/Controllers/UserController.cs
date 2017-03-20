using Bytes2you.Validation;
using System.Web.Mvc;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.userService = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ById(string id)
        {
            var model = this.userService.GetUserById(id);

            return View(model);
        }
    }
}