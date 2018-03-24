namespace JobPortal.DataAccess.Migrations
{
    using JobPortal.DataAccess.Helpers;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobPostThumbnailUrlToJobPostsTable : DbMigration
    {
        private readonly string _tableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobPostsTableName);
        private readonly string _urlColumnName = "JobPostThumbnailUrl";
        private readonly int _maxLength = 300;

        public override void Up()
        {
            AddColumn(this._tableName, this._urlColumnName, c => c.String(nullable: false, maxLength: this._maxLength));
        }
        
        public override void Down()
        {
            DropColumn(this._tableName, this._urlColumnName);
        }
    }
}
