using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public class Space
    {
        public int ID { get; set; }
        
        public string Name { get; set; }

        [Display (Name="Total Space")]
        public int SpaceTotal { get; set; }

        [Display (Name="Space Used")]
        public int SpaceUsed { get; set; }
        //public ApplicationUser PrincipleInvestigator { get; set; }
    }

    public class SpaceDbContext : DbContext
    {
        public DbSet<Space> Spaces { get; set; }
    }
}