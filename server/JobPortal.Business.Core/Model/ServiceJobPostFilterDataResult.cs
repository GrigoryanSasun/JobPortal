using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Business.Core.Model
{
    public class ServiceJobPostFilterDataResult: ServiceJobPostListResult
    {
        public IEnumerable<ServiceCategoryResult> JobCategories { get; set; }
        public IEnumerable<ServiceEmploymentTypeResult> EmploymentTypes { get; set; }
        public IEnumerable<ServiceLocationResult> Locations { get; set; }
    }
}
