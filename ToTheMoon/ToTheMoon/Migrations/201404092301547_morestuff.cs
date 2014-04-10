namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morestuff : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Requests", name: "Discriminator", newName: "Discriminator1");
            AlterColumn("dbo.Requests", "Discriminator", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            RenameColumn(table: "dbo.Requests", name: "Discriminator1", newName: "Discriminator");
        }
    }
}
