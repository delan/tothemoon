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
        public string Name { get; set; }

        [Required]
        public uint capacity { get; set; }

        [Required]
        public uint used { get; set; }

        [Required]
        public uint increase { get; set; }

        //[Required]
        public ApplicationUser PI { get; set; }
    }
}