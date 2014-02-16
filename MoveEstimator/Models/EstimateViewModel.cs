using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoveEstimator.Models
{
    public class EstimateViewModel
    {
        public IEnumerable<Estimate> Estimates { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}