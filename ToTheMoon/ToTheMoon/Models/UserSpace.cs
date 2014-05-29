using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTheMoon.Models
{
    public class UserSpace
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }

        [Key, Column(Order = 1)]
        public ApplicationUser user { get; set; }

        [Key]
        public Space space { get; set; }

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