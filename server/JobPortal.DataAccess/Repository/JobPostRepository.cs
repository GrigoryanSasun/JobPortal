using JobPortal.Common.Model;
using JobPortal.Context;
using JobPortal.DataAccess.Core.Contract;
using JobPortal.DataAccess.Core.Model;
using JobPortal.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Repository
{
    public class JobPostRepository : IJobPostRepository
    {
        private void AppendIdListCheckIfNotEmpty(StringBuilder sb, string tableNameAndColumn, int[] ids)
        {
            if ((ids != null) && (ids.Length > 0))
            {
                string idsConcatenated = DbHelpers.GetCommaSeparatedList<int>(ids);
                sb.AppendFormat(string.Format(@"
                        AND ({0} IN ({1}))
                ", tableNameAndColumn, idsConcatenated));
            }
        }

        private string GetBaseJobPostQuery(string selectStatement, RepositoryJobPostSearchInputModel searchInputModel, bool shouldSort, out object[] sqlParams)
        {
            var sqlListParams = new List<object>();
            StringBuilder sbQuery = new StringBuilder(string.Format(@"
                {0}
                FROM {1} jp
                JOIN {2} et ON jp.EmploymentTypeId = et.Id
                JOIN {3} jc ON jp.CategoryId = jc.Id
                JOIN {4} jt ON jp.TitleId = jt.Id
                JOIN {5} l ON jp.LocationId = l.Id
                WHERE 1=1
            ",
            selectStatement,
            DbHelpers.JobPostsTableName,
            DbHelpers.EmploymentTypeTableName,
            DbHelpers.JobCategoryTableName,
            DbHelpers.JobTitleTableName,
            DbHelpers.LocationTableName));
            var keywordSearchType = searchInputModel.KeywordSearchType;
            var keyword = searchInputModel.Keyword;
            if ((keywordSearchType != null) && (!string.IsNullOrWhiteSpace(keyword)))
            {
                keyword = FulltextHelper.PreprocessForFulltextSearch(keyword);
                string tablePrefixAndColumn = null;
                switch (keywordSearchType)
                {
                    case KeywordSearchType.JobCategory:
                        tablePrefixAndColumn = "jc.Name";
                        break;
                    case KeywordSearchType.Location:
                        tablePrefixAndColumn = "l.Address";
                        break;
                    case KeywordSearchType.Title:
                        tablePrefixAndColumn = "jt.Name";
                        break;
                }
                if (!string.IsNullOrEmpty(tablePrefixAndColumn))
                {
                    sbQuery.AppendFormat(@"
                        AND (CONTAINS({0}, @keyword))
                    ", tablePrefixAndColumn);
                    sqlListParams.Add(new SqlParameter("keyword", keyword));
                }
            }
            this.AppendIdListCheckIfNotEmpty(sbQuery, "jc.Id", searchInputModel.CategoryIds);
            this.AppendIdListCheckIfNotEmpty(sbQuery, "et.Id", searchInputModel.EmploymentTypeIds);
            this.AppendIdListCheckIfNotEmpty(sbQuery, "l.Id", searchInputModel.LocationIds);

            if (shouldSort)
            {
                var sortOrder = searchInputModel.SortOrder;

                if (sortOrder == null)
                {
                    sortOrder = JobPortal.Common.Model.SortOrder.ByCreatedDate;
                }

                string sortColumnName = "";
                if (sortOrder == JobPortal.Common.Model.SortOrder.ByViews)
                {
                    sortColumnName = "jp.Views";
                }
                else
                {
                    sortColumnName = "jp.CreatedAt";
                }
                sbQuery.AppendFormat(@"
                        ORDER BY {0} DESC
                        OFFSET {1} ROWS
                        FETCH NEXT {2} ROWS ONLY
                    ", 
                    sortColumnName,
                    searchInputModel.Skip,
                    searchInputModel.Take);
            }
            sqlParams = sqlListParams.ToArray();
            return sbQuery.ToString();
        }

        public int GetJobPostCount(RepositoryJobPostSearchInputModel searchInputModel)
        {
            using (var jobPortalDbContext = new JobPortalDbContext())
            {
                string selectStatement = "SELECT COUNT(*)";
                object[] sqlParams = null;
                string query = GetBaseJobPostQuery(selectStatement, searchInputModel, shouldSort: false, sqlParams: out sqlParams);
                var postCount = jobPortalDbContext.Database.SqlQuery<int>(query, sqlParams).First();
                return postCount;
            }
        }

        public IEnumerable<RepositoryJobPostResult> GetJobPosts(RepositoryJobPostSearchInputModel searchInputModel)
        {
            using (var jobPortalDbContext = new JobPortalDbContext())
            {
                string selectStatement = "SELECT jp.Id AS Id, jp.IsBookmarked, jp.JobPostThumbnailUrl, jp.Views, jp.CreatedAt, et.Name AS EmploymentType, jc.Name AS JobCategory, jt.Name AS Title, l.Address AS Location";
                object[] sqlParams = null;
                string query = GetBaseJobPostQuery(selectStatement, searchInputModel, shouldSort: true, sqlParams: out sqlParams);
                var result = jobPortalDbContext.Database.SqlQuery<RepositoryJobPostResult>(query, sqlParams).ToList();
                return result;
            }
        }
    }
}
