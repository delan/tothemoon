namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_fields_to_spaces1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Spaces", "Desc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Spaces", "Desc", c => c.String());
        }
    }
}
