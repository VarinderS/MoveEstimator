using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoveEstimator.Models;

namespace MoveEstimator.Controllers
{
    public class LocationsController : Controller
    {
        //
        // GET: /Locations/
		protected readonly Db db = new Db();

        public ActionResult Index()
        {
			var locations = db.Locations.ToList();

			var locationViewModel = new LocationViewModel();
			locationViewModel.LocationList = locations;

			return View(locationViewModel);
        }

		public ActionResult Update(Location location)
		{
			if (ModelState.IsValid)
			{
				db.Entry(location).State = EntityState.Modified;
				db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		public ActionResult Add(Location locationModel)
		{
			if (ModelState.IsValid)
			{
				if (db.Locations.Any( location => location.Name == locationModel.Name ))
				{
					TempData["AreadyExsists"] = "This location already exisits";
				}
				else
				{
					db.Locations.Add(locationModel);
					db.SaveChanges();
				}
			}

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int Id = 0)
		{
			var location = db.Locations.Find(Id);

			if (location == null) 
			{
				return HttpNotFound();
			}

			return View(location);
		}

		public ActionResult DeleteConfirmed(int Id = 0)
		{
			var location = db.Locations.Find(Id);

			if (location == null)
			{
				return HttpNotFound();
			}
			else
			{
				db.Locations.Remove(location);
				db.SaveChanges();

				return RedirectToAction("Index");
			}
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}

    }
}
