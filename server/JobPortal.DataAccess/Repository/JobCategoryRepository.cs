using JobPortal.Context;
using JobPortal.DataAccess.Core.Contract;
using JobPortal.DataAccess.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Repository
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        public IEnumerable<RepositoryCategoryResult> GetJobCategories()
        {
            using (var dbContext = new JobPortalDbContext())
            {
                return dbContext.JobCategories.OrderBy(jc => jc.Id).Select((jc) => new RepositoryCategoryResult
                {
                    Id = jc.Id,
                    Name = jc.Name
                }).ToList();
            }
        }
    }
}
