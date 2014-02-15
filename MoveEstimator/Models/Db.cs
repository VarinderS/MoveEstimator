using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoveEstimator.Models
{
	public class Db : DbContext
	{
		public DbSet<Estimate> Estimates { get; set; }
		public DbSet<Location> Locations { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Estimate>()
						.HasRequired( estimate => estimate.FromLocation )
						.WithMany()
						.HasForeignKey( estimate => estimate.FromLocationId )
						.WillCascadeOnDelete( false );
			
			modelBuilder.Entity<Estimate>()
						.HasRequired( estimate => estimate.ToLocation )  
						.WithMany()
						.HasForeignKey( estimate => estimate.ToLocationId )
						.WillCascadeOnDelete( false );
		}
	}
}