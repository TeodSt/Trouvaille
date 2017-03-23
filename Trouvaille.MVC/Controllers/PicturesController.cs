using Bytes2you.Validation;
using System.Collections.Generic;
using System.Web.Mvc;
using Trouvaille.Server.Models.Pictures;
using Trouvaille.Services.Common.Contracts;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class PicturesController : Controller
    {
        private readonly IMappingService mappingService;
        private readonly IPictureService pictureService;

        public PicturesController(
            IMappingService mappingService,
            IPictureService pictureService)
        {
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();
            Guard.WhenArgument(pictureService, "pictureService").IsNull().Throw();

            this.mappingService = mappingService;
            this.pictureService = pictureService;
        }
        
        public ActionResult Index()
        {
            var picturesFromDb = this.pictureService.GetAllPictures();

            var mappedPictures = this.mappingService.Map<IEnumerable<PictureViewModel>>(picturesFromDb);

            return View(mappedPictures);
        }
    }
}