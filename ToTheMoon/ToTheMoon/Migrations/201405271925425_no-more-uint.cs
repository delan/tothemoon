namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomoreuint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncreaseSpaceRequests", "increas", c => c.Int(nullable: false));
            AddColumn("dbo.Spaces", "capacity", c => c.Int(nullable: false));
            AddColumn("dbo.Spaces", "used", c => c.Int(nullable: false));
            AddColumn("dbo.Spaces", "increase", c => c.Int(nullable: false));
            AddColumn("dbo.NewSpaceRequests", "SpaceID", c => c.Int(nullable: false));
            AddColumn("dbo.NewSpaceRequests", "capacity", c => c.Int(nullable: false));
            AddColumn("dbo.NewSpaceRequests", "increase", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewSpaceRequests", "increase");
            DropColumn("dbo.NewSpaceRequests", "capacity");
            DropColumn("dbo.NewSpaceRequests", "SpaceID");
            DropColumn("dbo.Spaces", "increase");
            DropColumn("dbo.Spaces", "used");
            DropColumn("dbo.Spaces", "capacity");
            DropColumn("dbo.IncreaseSpaceRequests", "increas");
        }
    }
}
