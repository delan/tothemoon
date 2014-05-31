namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class things : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Spaces", "PIKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Spaces", "PIKey");
        }
    }
}
