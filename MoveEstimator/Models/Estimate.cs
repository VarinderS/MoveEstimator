using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoveEstimator.Models
{
	public class Estimate
	{
		public int Id { get; set; }

		[Display(Name = "Small Move")]
		public double SmallMove { get; set; }

		[Display(Name = "Medium Move")]
		public double MediumMove { get; set; }

		[Display(Name = "Large Move")]
		public double LargeMove { get; set; }

		[Display(Name="From Location")]
		public virtual Location FromLocation { get; set; }

		[Display(Name = "To Location")]
		public virtual Location ToLocation { get; set; }

		public int ToLocationId { get; set; }
		public int FromLocationId { get; set; }
	}
}