using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bytes2you.Validation;
using Trouvaille.Data;
using Trouvaille.Models;
using Trouvaille.Services;
using Trouvaille.Services.Contracts;

namespace Trouvaille.MVC.Controllers
{
    public class PlacesController : Controller
    {
        private TrouvailleContext db = new TrouvailleContext();

        private readonly IPlaceService placeService;

        public PlacesController(IPlaceService placeService)
        {
            Guard.WhenArgument(placeService, "placeService").IsNull().Throw();
            this.placeService = placeService;
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FounderId,CountryId,Description,Address,Longtitude,Latitude")] Place place)
        {
            if (ModelState.IsValid)
            {
                this.placeService.AddPlace(place);
                return RedirectToAction("Create");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", place.CountryId);
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
            db.SaveChanges();
            return RedirectToAction("Delete");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
