using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class LoginModel
    {
        public int CustomerId { get; set; }
        public string MobileNumber { get; set; }
        public int StoreId { get; set; }
        public string Flag { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}