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

        [HttpGet]
        public JsonResult GetPlaces()
        {
            var places = this.placeService.GetAllPlaces();

            return Json(places, JsonRequestBehavior.AllowGet);
        }


        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FounderId,CountryId,Description,Address,Longtitude,Latitude")] Place place)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        this.placeService.AddPlace(place);
        //        return RedirectToAction("Create");
        //    }

        //    ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", place.CountryId);
        //    return View(place);    

        //}
    }
}
