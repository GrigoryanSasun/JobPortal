namespace JobPortal.DataAccess.Migrations
{
    using JobPortal.DataAccess.Helpers;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateJobPostTable : DbMigration
    {
        private readonly string _tableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobPostsTableName);
        private readonly string _employmentTypeIndex;
        private readonly string _jobCategoryIndex;
        private readonly string _jobTitleIndex;
        private readonly string _locationIndex;
        private readonly string _employmentTypeColumn;
        private readonly string _jobCategoryColumn;
        private readonly string _jobTitleColumn;
        private readonly string _locationColumn;
        private readonly string _viewsColumn;
        private readonly string _createdAtColumn;
        private readonly string _employmentTypeTable;
        private readonly string _jobCategoryTable;
        private readonly string _jobTitleTable;
        private readonly string _locationTable;
        private readonly string _employmentTypeForeignKeyName;
        private readonly string _jobCategoryForeignKeyName;
        private readonly string _jobTitleForeignKeyName;
        private readonly string _locationForeignKeyName;

        private readonly string _viewsIndexName;
        private readonly string _createdAtIndexName;


        private void AddForeignKeyWithIndex(
            string dependantColumn, 
            string principalTable, 
            string foreignKeyName, 
            string indexName)
        {
            AddForeignKey(
                dependentTable: this._tableName,
                dependentColumn: dependantColumn,
                principalTable: principalTable,
                name: foreignKeyName
            );

            CreateIndex(
                    table: this._tableName,
                    column: dependantColumn,
                    name: indexName
            );
        }

        private void DropTableIndex(string indexName)
        {
            DropIndex(this._tableName, indexName);
        }

        public override void Up()
        {
            CreateTable(
                this._tableName,
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmploymentTypeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        TitleId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        IsBookmarked = c.Boolean(nullable: false),
                        Views = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            this.AddForeignKeyWithIndex(
                this._employmentTypeColumn, 
                this._employmentTypeTable, 
                this._employmentTypeForeignKeyName, 
                this._employmentTypeIndex
            );

            this.AddForeignKeyWithIndex(
                this._jobCategoryColumn,
                this._jobCategoryTable,
                this._jobCategoryForeignKeyName,
                this._jobCategoryIndex
            );

            this.AddForeignKeyWithIndex(
                this._jobTitleColumn,
                this._jobTitleTable,
                this._jobTitleForeignKeyName,
                this._jobTitleIndex
            );

            this.AddForeignKeyWithIndex(
                this._locationColumn,
                this._locationTable,
                this._locationForeignKeyName,
                this._locationIndex
            );

            CreateIndex(
                table: this._tableName,
                column: this._viewsColumn,
                name: this._viewsIndexName
            );

            CreateIndex(
                table: this._tableName,
                column: this._createdAtColumn,
                name: this._createdAtIndexName
            );
        }
        
        public override void Down()
        {
            DropTable(this._tableName);
        }

        public CreateJobPostTable()
        {
            this._employmentTypeColumn = "EmploymentTypeId";
            this._jobCategoryColumn = "CategoryId";
            this._jobTitleColumn = "TitleId";
            this._locationColumn = "LocationId";
            this._viewsColumn = "Views";
            this._createdAtColumn = "CreatedAt";
            this._employmentTypeIndex = DbHelpers.GetIndexPrefixedName("EmploymentType");
            this._jobCategoryIndex = DbHelpers.GetIndexPrefixedName("JobCategory");
            this._jobTitleIndex = DbHelpers.GetIndexPrefixedName("JobTitle");
            this._locationIndex = DbHelpers.GetIndexPrefixedName("Location");
            this._employmentTypeTable = DbHelpers.GetTableNameWithSchema(DbHelpers.EmploymentTypeTableName);
            this._jobCategoryTable = DbHelpers.GetTableNameWithSchema(DbHelpers.JobCategoryTableName);
            this._jobTitleTable = DbHelpers.GetTableNameWithSchema(DbHelpers.JobTitleTableName);
            this._locationTable = DbHelpers.GetTableNameWithSchema(DbHelpers.LocationTableName);
            this._employmentTypeForeignKeyName = DbHelpers.GetForeignKeyName(this._tableName, this._employmentTypeTable);
            this._jobCategoryForeignKeyName = DbHelpers.GetForeignKeyName(this._tableName, this._jobCategoryTable);
            this._jobTitleForeignKeyName = DbHelpers.GetForeignKeyName(this._tableName, this._jobTitleTable);
            this._locationForeignKeyName = DbHelpers.GetForeignKeyName(this._tableName, this._locationTable);
            this._viewsIndexName = DbHelpers.GetIndexPrefixedName("Views");
            this._createdAtIndexName = DbHelpers.GetIndexPrefixedName("CreatedAt");
        }
    }
}
