namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedrequeststate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IncreaseSpaceRequests", "status");
            DropColumn("dbo.NewSpaceRequests", "status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewSpaceRequests", "status", c => c.Int(nullable: false));
            AddColumn("dbo.IncreaseSpaceRequests", "status", c => c.Int(nullable: false));
        }
    }
}
