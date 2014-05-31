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
        public int capacity { get; set; }

        [Required]
        public int used { get; set; }

        [Required]
        public int increase { get; set; }

        //[Required]
        public virtual ApplicationUser PI { get; set; }

        public string PIKey { get; set; }
    }
}