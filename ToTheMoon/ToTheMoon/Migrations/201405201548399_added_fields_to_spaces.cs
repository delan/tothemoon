namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_fields_to_spaces : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Spaces", "SpaceName", c => c.String());
            AddColumn("dbo.Spaces", "ProjectID", c => c.Int(nullable: false));
            AddColumn("dbo.Spaces", "Desc", c => c.String());
            AddColumn("dbo.Spaces", "Description", c => c.String());
            DropColumn("dbo.Spaces", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Spaces", "Name", c => c.String());
            DropColumn("dbo.Spaces", "Description");
            DropColumn("dbo.Spaces", "Desc");
            DropColumn("dbo.Spaces", "ProjectID");
            DropColumn("dbo.Spaces", "SpaceName");
        }
    }
}
