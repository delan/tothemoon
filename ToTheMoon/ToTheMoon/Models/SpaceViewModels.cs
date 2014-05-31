using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public class SpaceUpdateCapacityViewModel
    {
        public int SpaceID { get; set; }

        public string Name { get; set; }

        [Display(Name = "New Capacity (GB)")]
        public int capacity { get; set; }

        [Display(Name = "New Used Space (GB)")]
        public int used { get; set; }
    }
}