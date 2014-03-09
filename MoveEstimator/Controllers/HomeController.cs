using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoveEstimator.Models;
using System.Data;
using AutoMapper;

namespace MoveEstimator.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
		private readonly Db db = new Db();

        public ActionResult Index(SearchViewModel search)
        {

			List<Location> Locations = db.Locations.ToList();

			IQueryable<Estimate> EstimateQuery = db.Estimates.AsQueryable();

			if (search.FromLocationId.GetValueOrDefault() != 0)
			{
				EstimateQuery = EstimateQuery.Where(estimate => estimate.FromLocationId == search.FromLocationId);
			}

			if (search.ToLocationId.GetValueOrDefault() != 0)
			{
				EstimateQuery = EstimateQuery.Where(estimate => estimate.ToLocationId == search.ToLocationId);
			}

			List<Estimate> EstimateList = EstimateQuery.ToList();

			List<EstimateViewModel> EstimateViewModelList = Mapper.Map<List<Estimate>, List<EstimateViewModel>>(EstimateList);
			
			var HomeViewModel = new HomeViewModel(EstimateViewModelList, search, Locations);

			return View(HomeViewModel);
        }

		[HttpPost]
		public ActionResult Update(EstimateViewModel estimateViewModel)
        {
			
			if ( ModelState.IsValid ) 
			{
				Estimate EstimateModel = Mapper.Map<EstimateViewModel, Estimate>(estimateViewModel);

				db.Entry(EstimateModel).State = EntityState.Modified;
				db.SaveChanges();
			}
            return RedirectToAction("Index");
        }

		[HttpPost]
		public ActionResult Create(Estimate addEstimate)
		{
			if (ModelState.IsValid)
			{
				Estimate EstimateModel = addEstimate;

				if ( db.Estimates.Any( e => e.FromLocationId == EstimateModel.FromLocationId && e.ToLocationId == EstimateModel.ToLocationId ) )
				{
					TempData["AlreadyExsists"] = "Alread exsists";
				}
				else
				{
					db.Estimates.Add( EstimateModel );
					db.SaveChanges();
				}

			}
			return RedirectToAction("Index");
		}

		public ActionResult	Delete(int id = 0)
		{
			Estimate EstimateModel = db.Estimates.Find(id);

			if ( EstimateModel == null ) 
			{
				return HttpNotFound();
			}
			return View(EstimateModel);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult	DeleteConfirmed(int id)
		{
			Estimate EstimateModel = db.Estimates.Find( id );

			db.Estimates.Remove( EstimateModel );
			db.SaveChanges();

			return RedirectToAction( "Index" );
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}

    }
}
