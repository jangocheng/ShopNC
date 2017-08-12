using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopNC.Web.Areas.Admin.Models
{
    public class Form
    {
        public byte Code { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
        public string Url { get; set; }
    }
}