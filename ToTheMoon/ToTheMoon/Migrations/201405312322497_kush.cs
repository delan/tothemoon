namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kush : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSpaces", "space_key", "dbo.Spaces");
            DropIndex("dbo.UserSpaces", new[] { "space_key" });
            DropColumn("dbo.UserSpaces", "spaceKey");
            RenameColumn(table: "dbo.UserSpaces", name: "space_key", newName: "spaceKey");
            AlterColumn("dbo.UserSpaces", "spaceKey", c => c.Int(nullable: false));
            AlterColumn("dbo.UserSpaces", "spaceKey", c => c.Int(nullable: false));
            CreateIndex("dbo.UserSpaces", "spaceKey");
            AddForeignKey("dbo.UserSpaces", "spaceKey", "dbo.Spaces", "key", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSpaces", "spaceKey", "dbo.Spaces");
            DropIndex("dbo.UserSpaces", new[] { "spaceKey" });
            AlterColumn("dbo.UserSpaces", "spaceKey", c => c.Int());
            AlterColumn("dbo.UserSpaces", "spaceKey", c => c.String());
            RenameColumn(table: "dbo.UserSpaces", name: "spaceKey", newName: "space_key");
            AddColumn("dbo.UserSpaces", "spaceKey", c => c.String());
            CreateIndex("dbo.UserSpaces", "space_key");
            AddForeignKey("dbo.UserSpaces", "space_key", "dbo.Spaces", "key");
        }
    }
}
