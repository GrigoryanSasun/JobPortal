using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Common.Model.Base
{
    public class JobPostResultBase
    {
        public int Id { get; set; }
        public string JobPostThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public bool IsBookmarked { get; set; }
        public int Views { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
