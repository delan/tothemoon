namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class things1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSpaces", "userKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSpaces", "userKey");
        }
    }
}
