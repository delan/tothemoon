namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notimestamp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "RequestTimestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "RequestTimestamp", c => c.DateTime(nullable: false));
        }
    }
}
