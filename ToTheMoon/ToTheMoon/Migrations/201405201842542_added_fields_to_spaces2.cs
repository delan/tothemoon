namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_fields_to_spaces2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Spaces", "SpaceName", c => c.String(nullable: false));
            AlterColumn("dbo.Spaces", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Spaces", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Spaces", "SpaceName", c => c.String(nullable: false));
        }
    }
}
