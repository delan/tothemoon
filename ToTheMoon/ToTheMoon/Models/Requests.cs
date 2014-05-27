using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;


namespace ToTheMoon.Models
{
    public class NewSpaceRequest
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Project ID")]
        public int SpaceID { get; set; }

        [Display(Name = "Brief")]
        public string brief { get; set; }

        [Display(Name = "Comment")]
        public string comment { get; set; }

        [Required]
        [Display(Name = "Capacity (GB)")]
        public int capacity { get; set; }

        [Required]
        [Display(Name = "Yearly Increase")]
        public int increase { get; set; }

        [Required]
        public ApplicationUser requester { get; set; }

        [Required]
        public DateTime timestamp { get; set; }
    }

    public class IncreaseSpaceRequest
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Space")]
        public Space space { get; set; }

        [Display(Name = "Brief")]
        public string brief { get; set; }

        [Display(Name = "Increase (GB)")]
        public int increas { get; set; }

        [Required]
        public ApplicationUser requester { get; set; }

        [Required]
        public DateTime timestamp { get; set; }
    }

}