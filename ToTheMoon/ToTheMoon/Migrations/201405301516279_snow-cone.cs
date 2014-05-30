namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class snowcone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncreaseSpaceRequests", "requester_key", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncreaseSpaceRequests", "requester_key");
        }
    }
}
