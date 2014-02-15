namespace MoveEstimator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estimates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmallMove = c.Double(nullable: false),
                        MediumMove = c.Double(nullable: false),
                        LargeMove = c.Double(nullable: false),
                        FromLocationId = c.Int(nullable: false),
                        ToLocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.FromLocationId)
                .ForeignKey("dbo.Locations", t => t.ToLocationId)
                .Index(t => t.FromLocationId)
                .Index(t => t.ToLocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Estimates", new[] { "ToLocationId" });
            DropIndex("dbo.Estimates", new[] { "FromLocationId" });
            DropForeignKey("dbo.Estimates", "ToLocationId", "dbo.Locations");
            DropForeignKey("dbo.Estimates", "FromLocationId", "dbo.Locations");
            DropTable("dbo.Locations");
            DropTable("dbo.Estimates");
        }
    }
}
