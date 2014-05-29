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
        public DbSet<UserSpace> UserSpaces { get; set; }
        public DbSet<NewSpaceRequest> NewSpaceRequests { get; set; }
        public DbSet<IncreaseSpaceRequest> IncreaseSpaceRequests { get; set; }

    }
}