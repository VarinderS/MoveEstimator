namespace MoveEstimator.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using MoveEstimator.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MoveEstimator.Models.Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoveEstimator.Models.Db context)
        {
			var locations = new List<Location>() {
								new Location() { Name = "Auckland" },
								new Location() { Name = "Whangarei" },
								new Location() { Name = "Palmerston North" },
								new Location() { Name = "Wellington" },
								new Location() { Name = "Christchurch" },
								new Location() { Name = "Otago" }
							};


			locations.ForEach( location => context.Locations.Add( location ) );
			context.SaveChanges();

			var estimates = new List<Estimate>() {
								new Estimate() { FromLocationId = 1, ToLocationId = 2, SmallMove = 100, MediumMove = 200, LargeMove = 300 },
								new Estimate() { FromLocationId = 1, ToLocationId = 3, SmallMove = 200, MediumMove = 300, LargeMove = 400 },
								new Estimate() { FromLocationId = 1, ToLocationId = 4, SmallMove = 300, MediumMove = 400, LargeMove = 500 },
								new Estimate() { FromLocationId = 1, ToLocationId = 5, SmallMove = 400, MediumMove = 500, LargeMove = 600 },
								new Estimate() { FromLocationId = 1, ToLocationId = 6, SmallMove = 500, MediumMove = 500, LargeMove = 700 },
								new Estimate() { FromLocationId = 2, ToLocationId = 1, SmallMove = 600, MediumMove = 600, LargeMove = 800 },
								new Estimate() { FromLocationId = 2, ToLocationId = 3, SmallMove = 700, MediumMove = 700, LargeMove = 900 },
								new Estimate() { FromLocationId = 2, ToLocationId = 4, SmallMove = 800, MediumMove = 800, LargeMove = 1000 }
							};

			estimates.ForEach( estimate => context.Estimates.Add( estimate ) );
			context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
