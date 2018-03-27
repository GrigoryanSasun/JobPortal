using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Web.ViewModel
{
    public class AngularSpaResponseViewModel
    {
        public IEnumerable<string> Scripts { get; set; }
        public IEnumerable<StylesheetData> Stylesheets { get; set; }
        public IEnumerable<ThemeData> Themes { get; set; }
    }
}