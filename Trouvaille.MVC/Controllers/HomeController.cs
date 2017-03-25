using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Trouvaille.Server.Models.Places;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IPlaceService placeService;

        public HomeController(IMappingService mappingService, IPlaceService placeService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(placeService, "placeService").IsNull().Throw();

            this.mappingService = mappingService;
            this.placeService = placeService;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetPlaces()
        {
            var places = this.placeService.GetAllPlaces().ToList();
            var model = this.mappingService.Map<IEnumerable<PlaceViewModel>>(places);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}