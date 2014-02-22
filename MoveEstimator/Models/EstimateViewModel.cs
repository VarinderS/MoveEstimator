using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoveEstimator.Models
{
    public class EstimateViewModel
    {
		public int EstimateId { get; set; }

		[Display(Name = "Small Move")]
		public double SmallMove { get; set; }

		[Display(Name = "Medium Move")]
		public double MediumMove { get; set; }

		[Display(Name = "Large Move")]
		public double LargeMove { get; set; }

		public int FromLocationId { get; set; }
		public int ToLocationId { get; set; }

		[Display(Name = "From Location")]
		public string FromLocationName { get; set; }

		[Display(Name = "To Location")]
		public string ToLocationName { get; set; }

        public IEnumerable<Location> Locations { get; set; }
    }
}