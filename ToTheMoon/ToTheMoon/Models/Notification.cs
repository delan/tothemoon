using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Desc { get; set; }

    }

}