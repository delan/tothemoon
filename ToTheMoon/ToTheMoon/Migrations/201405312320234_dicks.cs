namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dicks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSpaces", "spaceKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSpaces", "spaceKey");
        }
    }
}
