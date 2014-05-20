namespace ToTheMoon.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ToTheMoon.DAL.ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToTheMoon.DAL.ProjectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Notifications.AddOrUpdate(n => n.NotificationID,
            new Models.Notification()
            {

                Title = "Michael `Millie' Mroz (13333337)",
                Desc = "Cardiology 420 - now the Principal Investigator",
                NotificationID = 1
            });

            context.Spaces.AddOrUpdate(n => n.ID,
            new Models.Space()
            {
                SpaceName = "Software Components",
                Description = "Patrick's students who miss him",
                SpaceUsed = 27,
                SpaceTotal = 107,
                ID = 0
            });

        }
    }
}
