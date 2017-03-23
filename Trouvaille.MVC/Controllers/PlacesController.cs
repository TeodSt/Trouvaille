using System.Web.Mvc;
using Bytes2you.Validation;
using Trouvaille.Services.Contracts;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Server.Models.Places;
using System.Collections.Generic;

namespace Trouvaille.MVC.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly IMappingService mappingService;

        public PlacesController(IMappingService mappingService, IPlaceService placeService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(placeService, "placeService").IsNull().Throw();

            this.mappingService = mappingService;
            this.placeService = placeService;
        }
        
        public ActionResult Index()
        {
            var places = this.placeService.GetAllPlaces();

            var model = this.mappingService.Map<IEnumerable<PlaceViewModel>>(places);

            return View(model);
        }
    }
}
