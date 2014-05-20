using ToTheMoon.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;


namespace ToTheMoon.DAL
{
    public class ProjectContext : IdentityDbContext
    {
        public ProjectContext() : base("ProjectContext")
        {

        }

        public DbSet<Space> Spaces { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public System.Data.Entity.DbSet<ToTheMoon.Models.NewSpaceRequest> NewSpaceRequests { get; set; }
    }
}