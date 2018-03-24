using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Common.Model.Base
{
    public class JobPostListResultBase<T> where T:JobPostResultBase
    {
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<T> JobPosts { get; set; }
    }
}
