using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Common.Model.Base
{
    public class PaginatedJobPostInputModelBase : JobPostInputModelBase
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
