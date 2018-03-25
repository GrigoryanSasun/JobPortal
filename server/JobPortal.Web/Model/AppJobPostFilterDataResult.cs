using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Web.Model
{
    public class AppJobPostFilterDataResult: AppJobPostListResult
    {
        public IEnumerable<AppJobCategoryResult> JobCategories { get; set; }
        public IEnumerable<AppEmploymentTypeResult> EmploymentTypes { get; set; }
        public IEnumerable<AppLocationResult> Locations { get; set; }
    }
}