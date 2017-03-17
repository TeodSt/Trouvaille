using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trouvaille.Models;
using Trouvaille.Server.Models;
using Trouvaille.Server.Models.Places;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Areas.Private.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IPlaceService placesService;
        private readonly ICountryService countryService;

        public ProfileController(IMappingService mappingService, IPlaceService placesService, ICountryService countryService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(placesService, "placesService").IsNull().Throw();
            Guard.WhenArgument(countryService, "countryService").IsNull().Throw();

            this.mappingService = mappingService;
            this.placesService = placesService;
            this.countryService = countryService;
        }

        // GET: Private/Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Place()
        {
            AddPlaceViewModel model = new AddPlaceViewModel();

            var dbCountries = this.countryService.GetAllCountries().ToList();

          //  model.Countries = this.mappingService.Map<List<CountryViewModel>>(dbCountries);

            return PartialView("_CreatePlace");
        }

        [HttpPost]
        public ActionResult CreatePlace(AddPlaceViewModel model)
        {
            model.FounderId = this.User.Identity.GetUserId();
            model.CountryId = 3;

            var place = this.mappingService.Map<AddPlaceViewModel, Place>(model);

            this.placesService.AddPlace(place);
            return View("Index");
        }

        public ActionResult Photos()
        {
            return PartialView("_Photos");
        }
    }
}