namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothingchanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "HumanReadableRequestString");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "HumanReadableRequestString", c => c.String());
        }
    }
}
