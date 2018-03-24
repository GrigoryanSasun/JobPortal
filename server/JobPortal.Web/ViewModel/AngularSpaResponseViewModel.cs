using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Web.ViewModel
{
    public class AngularSpaResponseViewModel
    {
        public IEnumerable<string> JavascriptUrls { get; set; }
        public IEnumerable<string> StylesheetUrls { get; set; }
    }
}