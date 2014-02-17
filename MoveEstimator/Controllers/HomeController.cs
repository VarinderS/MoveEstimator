using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoveEstimator.Models;
using System.Data;

namespace MoveEstimator.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
		private readonly Db db = new Db();

        public ActionResult Index()
        {
			var estimates = db.Estimates.Include(e => e.FromLocation).Include(e => e.ToLocation).ToList();

            //ViewBag.FromLocationId = new SelectList(db.Locations.ToList(), "Id", "Name", db.Estimates.FirstOrDefault().FromLocationId);
			//ViewBag.ToLocationId = new SelectList( db.Locations, "Id", "Name", db.Estimates.FirstOrDefault().ToLocationId );

            // var estimateViewModel = new EstimateViewModel();

            // estimateViewModel.Estimates = estimates;
            // estimateViewModel.Locations = db.Locations.ToList(); //new SelectList( db.Locations, "Id", "Name" );

            // return View( estimateViewModel );

			var Locations = db.Locations.ToList();
			var EstimateViewModels = new List<EstimateViewModel>();

			estimates.ForEach( 
						estimate =>
						EstimateViewModels.Add( 
							new EstimateViewModel() 
							{ 
								EstimateId = estimate.Id,
								SmallMove = estimate.SmallMove,
								MediumMove = estimate.MediumMove,
								LargeMove = estimate.LargeMove,
								FromLocationId = estimate.FromLocationId,
								ToLocationId = estimate.ToLocationId,
								Locations = Locations
							})
						);

			return View(EstimateViewModels);
        }

        [HttpGet]
		public ActionResult Index( int FromLocationId = 0, int ToLocationId = 0 )
		{
			IQueryable<Estimate> filteredEstimates = db.Estimates;

			if ( FromLocationId != 0 ) 
			{
				filteredEstimates = filteredEstimates.Where( estimate => estimate.FromLocationId == FromLocationId );	
			}

			if ( ToLocationId != 0 )
			{
				filteredEstimates = filteredEstimates.Where( estimate => estimate.ToLocationId == ToLocationId );
			}

			// var filteredEstimates = db.Estimates.Where( estimate => estimate.FromLocationId == FromLocationId && estimate.ToLocationId == ToLocationId );//.ToList();

            // ViewBag.FromLocationId = new SelectList(db.Locations, "Id", "Name", FromLocationId);
            // ViewBag.ToLocationId = new SelectList(db.Locations, "Id", "Name", ToLocationId);

			var estimates = filteredEstimates.ToList();

			//var estimateViewModel = new EstimateViewModel();

			//estimateViewModel.Estimates = filteredEstimates;
			//estimateViewModel.Locations = db.Locations.ToList();

			var Locations = db.Locations.ToList();
			var EstimateViewModels = new List<EstimateViewModel>();

			estimates.ForEach(
						estimate =>
						EstimateViewModels.Add(
							new EstimateViewModel()
							{
								EstimateId = estimate.Id,
								SmallMove = estimate.SmallMove,
								MediumMove = estimate.MediumMove,
								LargeMove = estimate.LargeMove,
								FromLocationId = estimate.FromLocationId,
								ToLocationId = estimate.ToLocationId,
								Locations = Locations
							})
						);


			return View(EstimateViewModels);
		}

		[HttpPost]
        public ActionResult Edit( Estimate estimate )
        {
			if ( ModelState.IsValid ) 
			{
				db.Entry( estimate ).State = EntityState.Modified;
				db.SaveChanges();
			}
            return RedirectToAction("Index");
        }

		[HttpPost]
		public ActionResult Create( Estimate estimate )
		{
			if ( ModelState.IsValid )
			{
				db.Estimates.Add( estimate );
				db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

    }
}
