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
        [Range(1, int.MaxValue)]
        [Display(Name = "Capacity")]
        public int capacity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Yearly Increase")]
        public int increase { get; set; }

        //[Required]
        public ApplicationUser requester { get; set; }
        public string requester_key { get; set; }

        //[Required]
        [Display(Name = "Timestamp")]
        public DateTime timestamp { get; set; }
    }

    public class NewSpaceRequestCommentViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Project Name")]
        public string name { get; set; }

        [Display(Name = "Brief")]
        public string brief { get; set; }

        [Display(Name = "Comment")]
        public string comment { get; set; }

        [Display(Name = "Capacity")]
        public int capacity { get; set; }

        [Display(Name = "Yearly Increase")]
        public int increase { get; set; }

        [Display(Name = "Requester")]
        public ApplicationUser requester { get; set; }

        public string requester_key { get; set; }
    }

    public class IncreaseSpaceRequest
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Project ID")]
        public int SpaceID { get; set; }

        [Display(Name = "Space")]
        public Space space { get; set; }


        [Display(Name = "Brief")]
        public string brief { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Increase")]
        public int increase { get; set; }

        //[Required]
        public virtual ApplicationUser requester { get; set; }
        public string requester_key { get; set; }

        //[Required]

        [Display(Name = "Timestamp")]
        public DateTime timestamp { get; set; }
    }

}