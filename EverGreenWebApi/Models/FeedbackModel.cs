using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public string MobileNumber { get; set; }
        public int StoreId { get; set; }
        public decimal Feedback { get; set; }
        public string CustomerType { get; set; }
    }
}