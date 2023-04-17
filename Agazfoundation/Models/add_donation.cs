using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agazfoundation.Models
{
        
    public class add_donation
    {
        [AllowHtml]
        public string id { get; set; }
        public string fname { get; set; }

        public string lname { get; set; }
        public string fathername { get; set; }
        public string mothername { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string amount { get; set; }
        public string amount_type { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public string imageurl { get; set; }
        public string total_amount { get; set; }
        public HttpPostedFileBase fileupload { get; set; }
    }

    public class agencyfillter
    {
        public string countryname { get; set; }
        public string name { get; set; }
        public string departurefrom { get; set; }

        public string status { get; set; }

    }
}