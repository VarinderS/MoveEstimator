using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveEstimator.Models
{
	public class LocationViewModel
	{
		public LocationViewModel()
		{
			this.LocationModel = new Location();
		}
		public IEnumerable<Location> LocationList { get; set; }
		public Location LocationModel { get; set; }
	}
}