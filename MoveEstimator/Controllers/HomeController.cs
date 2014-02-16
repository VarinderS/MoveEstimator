using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoveEstimator.Models;

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

            var estimateViewModel = new EstimateViewModel();

            estimateViewModel.Estimates = estimates;
            estimateViewModel.Locations = db.Locations.ToList(); //new SelectList( db.Locations, "Id", "Name" );

            return View( estimateViewModel );
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

            var estimateViewModel = new EstimateViewModel();

            estimateViewModel.Estimates = filteredEstimates;
            estimateViewModel.Locations = db.Locations.ToList();

			return View( estimateViewModel );
		}

		[HttpPost]
		public ActionResult Create( Estimate estimate )
		{
			
			return View();
		}

    }
}
