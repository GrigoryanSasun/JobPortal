namespace JobPortal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateJobPostsView : DbMigration
    {
        private readonly string _viewName = "JobPostView";

        public override void Up()
        {
            var employmentTable = DbHelpers.GetTableNameWithSchema(DbHelpers.EmploymentTypeTableName);
            var jobCategoryTable = DbHelpers.GetTableNameWithSchema(DbHelpers.JobCategoryTableName);
            var jobTitleTable = DbHelpers.GetTableNameWithSchema(DbHelpers.JobTitleTableName);
            var locationTable = DbHelpers.GetTableNameWithSchema(DbHelpers.LocationTableName);
            var jobPostsTable = DbHelpers.GetTableNameWithSchema(DbHelpers.JobPostsTableName);
            Sql(string.Format(@"
                CREATE VIEW {0}
                WITH SCHEMABINDING
                AS
                    SELECT {5}.Id AS Id, {1}.Name AS EmploymentType, {1}.Id AS EmploymentTypeId, {2}.Name AS Category, {2}.Id AS CategoryId, {3}.Name AS Title, {3}.Id AS TitleId, {4}.Address AS Location, {4}.Id AS LocationId
                    FROM {5}
                    JOIN {1} ON {5}.EmploymentTypeId = {1}.Id
                    JOIN {2} ON {5}.CategoryId = {2}.Id
                    JOIN {3} ON {5}.TitleId = {3}.Id
                    JOIN {4} ON {5}.LocationId = {4}.Id
            ", this._viewName, employmentTable, jobCategoryTable, jobTitleTable, locationTable, jobPostsTable));
        }
        
        public override void Down()
        {
            Sql(string.Format(@"DROP VIEW {0}", this._viewName));
        }
    }
}
