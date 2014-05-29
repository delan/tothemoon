namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nameconflicts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSpaces",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        role = c.Int(nullable: false),
                        space_key = c.Int(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Spaces", t => t.space_key)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.space_key)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSpaces", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSpaces", "space_key", "dbo.Spaces");
            DropIndex("dbo.UserSpaces", new[] { "user_Id" });
            DropIndex("dbo.UserSpaces", new[] { "space_key" });
            DropTable("dbo.UserSpaces");
        }
    }
}
