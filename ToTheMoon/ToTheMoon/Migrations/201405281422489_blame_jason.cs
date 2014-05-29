namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blame_jason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncreaseSpaceRequests", "SpaceID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncreaseSpaceRequests", "SpaceID");
        }
    }
}
