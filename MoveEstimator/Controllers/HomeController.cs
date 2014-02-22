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

        public ActionResult Index( SearchViewModel searchViewModel )
        {
			var homeViewModel = new HomeViewModel();

			var locations = db.Locations.ToList();

			var estimates = db.Estimates.AsQueryable();

			if (searchViewModel.FromLocationId != 0 && searchViewModel.FromLocationId != null)
			{
				estimates = estimates.Where(estimate => estimate.FromLocationId == searchViewModel.FromLocationId);
			}

			if (searchViewModel.ToLocationId != 0 && searchViewModel.ToLocationId != null)
			{
				estimates = estimates.Where(estimate => estimate.ToLocationId == searchViewModel.ToLocationId);
			}
			
			if ( searchViewModel.Locations == null )
			{
				searchViewModel.Locations = locations;
			}

			var estimateList = estimates.ToList();

			var estimateViewModelList = new List<EstimateViewModel>();

			estimateList.ForEach(estimate => estimateViewModelList.Add(new EstimateViewModel
																			{
																				EstimateId = estimate.Id,
																				SmallMove = estimate.SmallMove,
																				MediumMove = estimate.MediumMove,
																				LargeMove = estimate.LargeMove,
																				FromLocationId = estimate.FromLocationId,
																				ToLocationId = estimate.ToLocationId,
																				FromLocationName = estimate.FromLocation.Name,
																				ToLocationName = estimate.ToLocation.Name,
																				Locations = locations
																			}));



			homeViewModel.EstimateViewModelList = estimateViewModelList;
			homeViewModel.searchViewModel = searchViewModel;
			homeViewModel.AddEstimate = new Estimate();
			homeViewModel.Locations = locations;

			return View( homeViewModel );
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
		public ActionResult Create(Estimate AddEstimate)
		{
			if (ModelState.IsValid)
			{
				Estimate estimate = AddEstimate;

				if ( db.Estimates.Any( e => e.FromLocationId == estimate.FromLocationId && e.ToLocationId == estimate.ToLocationId ) )
				{
					TempData["AlreadyExsists"] = "Alread exsists";
				}
				else
				{
					db.Estimates.Add( estimate );
					db.SaveChanges();
				}

			}
			return RedirectToAction("Index");
		}

    }
}
