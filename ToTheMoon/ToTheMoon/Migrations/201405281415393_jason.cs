namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jason : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncreaseSpaceRequests", "space_key", "dbo.Spaces");
            DropIndex("dbo.IncreaseSpaceRequests", new[] { "space_key" });
            AlterColumn("dbo.IncreaseSpaceRequests", "space_key", c => c.Int());
            CreateIndex("dbo.IncreaseSpaceRequests", "space_key");
            AddForeignKey("dbo.IncreaseSpaceRequests", "space_key", "dbo.Spaces", "key");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncreaseSpaceRequests", "space_key", "dbo.Spaces");
            DropIndex("dbo.IncreaseSpaceRequests", new[] { "space_key" });
            AlterColumn("dbo.IncreaseSpaceRequests", "space_key", c => c.Int(nullable: false));
            CreateIndex("dbo.IncreaseSpaceRequests", "space_key");
            AddForeignKey("dbo.IncreaseSpaceRequests", "space_key", "dbo.Spaces", "key", cascadeDelete: true);
        }
    }
}
