using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Common.Model.Base
{
    public class JobPostInputModelBase
    {
        public KeywordSearchType? KeywordSearchType { get; set; }
        public SortOrder? SortOrder { get; set; }
        public string Keyword { get; set; }

        public int[] CategoryIds { get; set; }
        public int[] EmploymentTypeIds { get; set; }
        public int[] LocationIds { get; set; }
    }
}
