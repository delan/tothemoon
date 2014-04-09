using ToTheMoon.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ToTheMoon.DAL
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("ProjectContext")
        {

        }

        public DbSet<Space> Spaces { get; set; }
    }
}