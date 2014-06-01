using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity;
  
namespace ToTheMoon.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display (Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display (Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public GlobalRole role { get; set; }

        [Required]
        public Faculty faculty { get; set; }
    }

    public enum GlobalRole
    {
        REGULAR,
        APPROVER,
        ADMIN
    }
    public enum Faculty
    {
        SAE,
        HUMANITIES,
        BUSINESS,
        HEALTHSCI
    }
}