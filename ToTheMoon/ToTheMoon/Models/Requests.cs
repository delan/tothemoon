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
        public uint SpaceID { get; set; }

        [Display(Name = "Brief")]
        public string brief { get; set; }

        [Display(Name = "Comment")]
        public string comment { get; set; }

        [Required]
        [Display(Name = "Capacity (GB)")]
        public uint capacity { get; set; }

        [Required]
        [Display(Name = "Yearly Increase")]
        public uint increase { get; set; }

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
        public uint increas { get; set; }

        [Required]
        public ApplicationUser requester { get; set; }

        [Required]
        public DateTime timestamp { get; set; }
    }

}