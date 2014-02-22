using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveEstimator.Models
{
	public class HomeViewModel
	{
		public SearchViewModel searchViewModel { get; set; }
		public IEnumerable<EstimateViewModel> EstimateViewModelList { get; set; }
		public Estimate AddEstimate { get; set; }
		public IEnumerable<Location> Locations { get; set; }
	}
}