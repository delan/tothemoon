using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTheMoon.Models
{
    public class UserSpace
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }

        [Key, Column(Order = 1)]
        public virtual ApplicationUser user { get; set; }

        public string userKey { get; set; }

        [Key]
        public virtual Space space { get; set; }

        public int spaceKey { get; set; }

        [Required]
        public SpaceRole role { get; set; }
    }

    public enum SpaceRole
    {
        [Description("Read-Only Researcher")]
        COLLAB_RO,
        [Description("Read/Write Reasearcher")]
        COLLAB_RW,
        [Description("Data Manager")]
        DATAMANAGER
    }
}