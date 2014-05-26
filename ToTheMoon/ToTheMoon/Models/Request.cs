using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public abstract class Request
    {
        public int RequestID { get; set; }

        [Display(Name = "Time Requested")]
        public DateTime RequestTimestamp { get; set; }

        [Display(Name = "Requested by")]
        public ApplicationUser Requester { get; set; }

        [Display(Name = "Request")]
        public abstract string HumanReadableRequestString { get; set; }

        public bool? Approved { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        //Constructor
        public Request()
        {
            Comment = "";
            Approved = null;
        }
    }

    public class NewSpaceRequest : Request
    {
        [Required]
        [Display (Name = "Project Name")]
        public string SpaceName { get; set; }

        [Required]
        [Display (Name = "Project ID")]
        public int ProjectID { get; set; }

        [Required]
        [Display(Name = "Project Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Storage Required (GB)")]
        public int StorageTotal { get; set; }

        [Required]
        [Display(Name = "Storage Used (GB)")]
        public int StorageUsed { get; set; }

        [Required]
        [Display(Name = "Projected Yearly Growth (GB)")]
        public int YearlyGrowth { get; set; }

        public override string HumanReadableRequestString
        {
            get
            {
                return String.Format("New space request for {0} ({1}GB).", SpaceName, StorageTotal);
            }

            set
            {
                return;
            }
        }
    }
}