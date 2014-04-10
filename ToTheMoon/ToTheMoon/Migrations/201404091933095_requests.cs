namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestID = c.Int(nullable: false, identity: true),
                        RequestTimestamp = c.DateTime(nullable: false),
                        SpaceName = c.String(),
                        ProjectID = c.Int(),
                        Description = c.String(),
                        StorageTotal = c.Int(),
                        StorageUsed = c.Int(),
                        YearlyGrowth = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RequestID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Requests");
        }
    }
}
