namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class everything : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncreaseSpaceRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpaceID = c.Int(nullable: false),
                        brief = c.String(),
                        increase = c.Int(nullable: false),
                        timestamp = c.DateTime(nullable: false),
                        requester_Id = c.String(maxLength: 128),
                        space_key = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.requester_Id)
                .ForeignKey("dbo.Spaces", t => t.space_key)
                .Index(t => t.requester_Id)
                .Index(t => t.space_key);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        role = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Spaces",
                c => new
                    {
                        key = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        capacity = c.Int(nullable: false),
                        used = c.Int(nullable: false),
                        increase = c.Int(nullable: false),
                        PI_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.key)
                .ForeignKey("dbo.AspNetUsers", t => t.PI_Id)
                .Index(t => t.PI_Id);
            
            CreateTable(
                "dbo.NewSpaceRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        SpaceID = c.Int(nullable: false),
                        brief = c.String(),
                        comment = c.String(),
                        capacity = c.Int(nullable: false),
                        increase = c.Int(nullable: false),
                        requester_key = c.String(),
                        timestamp = c.DateTime(nullable: false),
                        requester_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.requester_Id)
                .Index(t => t.requester_Id);
            
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
            DropForeignKey("dbo.NewSpaceRequests", "requester_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IncreaseSpaceRequests", "space_key", "dbo.Spaces");
            DropForeignKey("dbo.Spaces", "PI_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IncreaseSpaceRequests", "requester_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSpaces", new[] { "user_Id" });
            DropIndex("dbo.UserSpaces", new[] { "space_key" });
            DropIndex("dbo.NewSpaceRequests", new[] { "requester_Id" });
            DropIndex("dbo.Spaces", new[] { "PI_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.IncreaseSpaceRequests", new[] { "space_key" });
            DropIndex("dbo.IncreaseSpaceRequests", new[] { "requester_Id" });
            DropTable("dbo.UserSpaces");
            DropTable("dbo.NewSpaceRequests");
            DropTable("dbo.Spaces");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.IncreaseSpaceRequests");
        }
    }
}
