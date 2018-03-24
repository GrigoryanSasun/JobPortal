namespace JobPortal.DataAccess.Migrations
{
    using JobPortal.DataAccess.Helpers;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFullTextIndexes : DbMigration
    {
        private readonly string _fulltextCatalogName = "JobPortalFullTextCatalog";
        private readonly string _jobCategoryTableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobCategoryTableName);
        private readonly string _jobTitleTableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobTitleTableName);
        private readonly string _locationTableName = DbHelpers.GetTableNameWithSchema(DbHelpers.LocationTableName);

        public override void Up()
        {
            Sql(string.Format(@"
                CREATE FULLTEXT CATALOG {0};
            ", this._fulltextCatalogName), suppressTransaction: true);

            string createFullTextIndexNameQuery = @"
                CREATE FULLTEXT INDEX ON {0}(Name) KEY INDEX {1} ON {2};
            ";
            string createFullTextIndexAddressQuery = @"
                CREATE FULLTEXT INDEX ON {0}(Address) KEY INDEX {1} ON {2};
            ";
            // Job category name full text index
            Sql(string.Format(createFullTextIndexNameQuery, this._jobCategoryTableName, DbHelpers.JobCategoryUniqueNameIndex, this._fulltextCatalogName), suppressTransaction: true);

            // Job title full text index
            Sql(string.Format(createFullTextIndexNameQuery, this._jobTitleTableName, DbHelpers.JobTitleUniqueNameIndex, this._fulltextCatalogName), suppressTransaction: true);

            // Location full text index
            Sql(string.Format(createFullTextIndexAddressQuery, this._locationTableName, DbHelpers.LocationUniqueAddressIndex, this._fulltextCatalogName), suppressTransaction: true);

        }

        public override void Down()
        {
            Sql(string.Format(@"
                DROP FULLTEXT INDEX ON {0};
                DROP FULLTEXT INDEX ON {1};
                DROP FULLTEXT INDEX ON {2};
                DROP FULLTEXT CATALOG {3};
            ", 
            this._jobCategoryTableName, 
            this._jobTitleTableName, 
            this._locationTableName, 
            this._fulltextCatalogName
            ), suppressTransaction: true);
        }
    }
}
