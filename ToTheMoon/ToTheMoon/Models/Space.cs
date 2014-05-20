using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public class Space
    {
        public int ID { get; set; }

        [Required]
        public string SpaceName { get; set; }

        public int ProjectID { get; set; }

        public string Description { get; set; }

        [Display (Name="Total Space")]
        public int SpaceTotal { get; set; }

        [Display (Name="Space Used")]
        public int SpaceUsed { get; set; }
        //public ApplicationUser PrincipalInvestigator { get; set; }
    }
}
