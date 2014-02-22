using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveEstimator.Models
{
	public class SearchViewModel
	{
		
		public int? FromLocationId { get; set; }
		public int? ToLocationId { get; set; }

		public IEnumerable<Location> Locations { get; set; }
	}
}