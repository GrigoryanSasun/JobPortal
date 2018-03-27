using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Web
{
    public class DeserializedTheme
    {
        public string id { get; set; }
        public bool is_default { get; set; }
        public string filename { get; set; }
        public string ui_main_color { get; set; }
        public string ui_description { get; set; }
    }
}