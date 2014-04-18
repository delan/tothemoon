namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "RequestTimestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "RequestTimestamp");
        }
    }
}
