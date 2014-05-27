using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public class UserSpace
    {
        [Key]
        public ApplicationUser user { get; set; }

        [Key]
        public Space spacev { get; set; }

        [Required]
        public SpaceRole role { get; set; }
    }

    public enum SpaceRole
    {
        COLLAB_RO,
        COLLAB_RW,
        DATAMANAGER
    }
}