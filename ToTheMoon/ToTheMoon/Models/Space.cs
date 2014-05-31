using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public class Space
    {
        [Key]
        public int key { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public int capacity { get; set; }

        [Required]
        [Display(Name = "Used Space")]
        public int used { get; set; }

        [Required]
        [Display(Name = "Yearly Increase")]
        public int increase { get; set; }

        //[Required]
        [Display(Name = "Principal Investigator")]
        public virtual ApplicationUser PI { get; set; }

        public string PIKey { get; set; }
    }
}