namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Request_Comments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Approved", c => c.Boolean());
            AddColumn("dbo.Requests", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Comment");
            DropColumn("dbo.Requests", "Approved");
        }
    }
}
