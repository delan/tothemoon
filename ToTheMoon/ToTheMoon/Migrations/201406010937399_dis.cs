namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "faculty", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "faculty");
        }
    }
}
