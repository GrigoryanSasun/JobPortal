using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess
{
    static class DbHelpers
    {
        public const string SchemaName = "dbo";
        public const string EmploymentTypeTableName = "EmploymentType";
        public const string JobCategoryTableName = "JobCategory";
        public const string JobTitleTableName = "JobTitle";

        public static string GetTableNameWithSchema(string tableName)
        {
            return SchemaName + "." + tableName;
        }
    }
}
