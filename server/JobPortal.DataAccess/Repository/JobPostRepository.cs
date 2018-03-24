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
    public class JobPostRepository : IJobPostRepository
    {
        public int GetJobPostCount(RepositoryJobPostSearchInputModel searchInputModel)
        {
            using (var jobPortalDbContext = new JobPortalDbContext())
            {
                var jobPostTableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobPostsTableName);
                var postCount = jobPortalDbContext.Database.SqlQuery<int>(string.Format("SELECT COUNT(*) FROM {0}", jobPostTableName)).First();
                return postCount;
            }
        }

        public IEnumerable<RepositoryJobPostResult> GetJobPosts(RepositoryJobPostSearchInputModel searchInputModel)
        {
            using (var jobPortalDbContext = new JobPortalDbContext())
            {
                string query = @"
                SELECT jp.Id AS Id, jp.IsBookmarked, jp.JobPostThumbnailUrl, jp.Views, jp.CreatedAt, et.Name AS EmploymentType, jc.Name AS JobCategory, jt.Name AS Title, l.Address AS Location
                FROM JobPost jp
                JOIN EmploymentType et ON jp.EmploymentTypeId = et.Id
                JOIN JobCategory jc ON jp.CategoryId = jc.Id
                JOIN JobTitle jt ON jp.TitleId = jt.Id
                JOIN Location l ON jp.LocationId = l.Id;
                ";
                var result = jobPortalDbContext.Database.SqlQuery<RepositoryJobPostResult>(query).ToList();
                return result;
            }
        }
    }
}
