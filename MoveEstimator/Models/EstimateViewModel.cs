using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoveEstimator.Models
{
    public class EstimateViewModel
    {
		public int EstimateId { get; set; }
		public double SmallMove { get; set; }
		public double MediumMove { get; set; }
		public double LargeMove { get; set; }

		public int FromLocationId { get; set; }
		public int ToLocationId { get; set; }

        public IEnumerable<Estimate> Estimates { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}