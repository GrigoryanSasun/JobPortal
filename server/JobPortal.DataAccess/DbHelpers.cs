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
        public const string LocationTableName = "Location";
        public const string JobPostsTableName = "JobPost";

        public static string GetTableNameWithSchema(string tableName)
        {
            return SchemaName + "." + tableName;
        }

        public static string GetForeignKeyName(string dependantTable, string principalTable)
        {
            return "FK_" + dependantTable + "_" + principalTable;
        }

        public static string GetIndexPrefixedName(string name, bool isUnique = false)
        {
            string prefix = isUnique ? "UX" : "IX";
            return prefix + "_" + name;
        }
    }
}
