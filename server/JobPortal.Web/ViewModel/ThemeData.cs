using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Web.ViewModel
{
    public class ThemeData
    {
        public string ThemeId { get; set; }
        public bool IsDefault { get; set; }
        public string UIMainColor { get; set; }
        public string StylesheetUrl { get; set; }
    }
}