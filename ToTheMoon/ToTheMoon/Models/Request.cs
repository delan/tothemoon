using System;
using System.ComponentModel.DataAnnotations;

namespace ToTheMoon.Models
{
    public abstract class Request
    {
        [Key]
        public int RequestID { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public ApplicationUser Requester { get; set; }

        public abstract string HumanReadableRequestString { get; }
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
        }
    }

    /*
    public class RequestListViewModel
    {
        List<RequestListViewModelItem> Requests;
        private static ProjectContext db = new ProjectContext();

        public RequestListViewModel()
        {
            // get all request types.
            IEnumerable requests = db.Requests.AsEnumerable();
            List<RequestListViewModelItem> result;

            foreach(var request in requests)
            {
                RequestListViewModelItem currentListItem = new RequestListViewModelItem();

                if (request is NewSpaceRequest)
                {
                    NewSpaceRequest typedRequest = (NewSpaceRequest)request;
                    currentListItem.Request = typedRequest;
                    currentListItem.HumanReadableRequest = String.Format("New storage request for {0} ({1}GB)", typedRequest.SpaceName, typedRequest.StorageTotal);
                }

                Requests.Add(currentListItem);
            }
        }

        public class RequestListViewModelItem
        {
            public Request Request;
            public string HumanReadableRequest;
        }
    }*/
}