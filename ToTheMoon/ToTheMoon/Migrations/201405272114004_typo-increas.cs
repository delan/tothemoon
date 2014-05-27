namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typoincreas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncreaseSpaceRequests", "increase", c => c.Int(nullable: false));
            DropColumn("dbo.IncreaseSpaceRequests", "increas");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncreaseSpaceRequests", "increas", c => c.Int(nullable: false));
            DropColumn("dbo.IncreaseSpaceRequests", "increase");
        }
    }
}
