using System.Data.Entity;

namespace ToTheMoon.Models
{
    public class Space
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int SpaceTotal { get; set; }
        public int SpaceUsed { get; set; }
        //public ApplicationUser PrincipleInvestigator { get; set; }
    }

    public class SpaceDbContext : DbContext
    {
        public DbSet<Space> Spaces { get; set; }
    }
}