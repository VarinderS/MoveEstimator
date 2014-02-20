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

        public ActionResult Index( EstimateViewModel estimateViewModel )
        {
			IQueryable<Estimate> estimates = db.Estimates.AsQueryable();

			if ( estimateViewModel.FromLocationId != 0 )
			{
				estimates = estimates.Where( estimate => estimate.FromLocationId == estimateViewModel.FromLocationId );
			}

			if ( estimateViewModel.ToLocationId != 0 ) 
			{
				estimates = estimates.Where( estimate => estimate.ToLocationId == estimateViewModel.ToLocationId );
			}

			var estimatesList = estimates.ToList();

			var Locations = db.Locations.ToList();
			var EstimateViewModels = new List<EstimateViewModel>();

			estimatesList.ForEach( 
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
        public ActionResult Update( EstimateViewModel estimateViewModel )
        {
			if ( ModelState.IsValid ) 
			{
				Estimate estimate = new Estimate { 
										Id = estimateViewModel.EstimateId,
										FromLocationId = estimateViewModel.FromLocationId,
										ToLocationId = estimateViewModel.ToLocationId,
										SmallMove = estimateViewModel.SmallMove,
										MediumMove = estimateViewModel.MediumMove,
										LargeMove = estimateViewModel.LargeMove
									};
				db.Entry(estimate).State = EntityState.Modified;
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
