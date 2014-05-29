namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_fields_to_spaces4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Spaces", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Spaces", "Description", c => c.String(nullable: false));
        }
    }
}
