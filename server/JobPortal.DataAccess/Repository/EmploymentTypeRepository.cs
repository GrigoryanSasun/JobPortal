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
    public class EmploymentTypeRepository : IEmploymentTypeRepository
    {
        public IEnumerable<RepositoryEmploymentTypeResult> GetEmploymentTypes()
        {
            using (var dbContext = new JobPortalDbContext())
            {
                return dbContext.EmploymentTypes.Select((et) => new RepositoryEmploymentTypeResult
                {
                    Id = et.Id,
                    Name = et.Name
                }).ToList();
            }
        }
    }
}
