using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class FeedbackDetailsModel
    {
        public int CustomerId { get; set; }
        public string MobileNumber { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int FeedbackId { get; set; }
        public decimal Feedback { get; set; }
    }
}