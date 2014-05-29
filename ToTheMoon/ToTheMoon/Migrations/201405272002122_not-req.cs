namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notreq : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncreaseSpaceRequests", "requester_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NewSpaceRequests", "requester_Id", "dbo.AspNetUsers");
            DropIndex("dbo.IncreaseSpaceRequests", new[] { "requester_Id" });
            DropIndex("dbo.NewSpaceRequests", new[] { "requester_Id" });
            AlterColumn("dbo.IncreaseSpaceRequests", "requester_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.NewSpaceRequests", "requester_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.IncreaseSpaceRequests", "requester_Id");
            CreateIndex("dbo.NewSpaceRequests", "requester_Id");
            AddForeignKey("dbo.IncreaseSpaceRequests", "requester_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.NewSpaceRequests", "requester_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewSpaceRequests", "requester_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IncreaseSpaceRequests", "requester_Id", "dbo.AspNetUsers");
            DropIndex("dbo.NewSpaceRequests", new[] { "requester_Id" });
            DropIndex("dbo.IncreaseSpaceRequests", new[] { "requester_Id" });
            AlterColumn("dbo.NewSpaceRequests", "requester_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.IncreaseSpaceRequests", "requester_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.NewSpaceRequests", "requester_Id");
            CreateIndex("dbo.IncreaseSpaceRequests", "requester_Id");
            AddForeignKey("dbo.NewSpaceRequests", "requester_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncreaseSpaceRequests", "requester_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
