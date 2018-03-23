using JobPortal.DataAccess.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Model
{
    [Table(name: DbHelpers.JobPostsTableName, Schema = DbHelpers.SchemaName)]
    class JobPost: ModelBase
    {
        public int EmploymentTypeId { get; set; }
        public int CategoryId { get; set; }
        public int TitleId { get; set; }
        public int LocationId { get; set; }
        public bool IsBookmarked { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Views { get; set; }
    }
}
