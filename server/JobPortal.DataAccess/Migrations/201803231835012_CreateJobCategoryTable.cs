namespace JobPortal.DataAccess.Migrations
{
    using JobPortal.DataAccess.Helpers;
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateJobCategoryTable : DbMigration
    {
        private readonly string _tableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobCategoryTableName);
        private readonly string _nameColumn = "Name";
        private readonly string _uniqueNameIndex = DbHelpers.JobCategoryUniqueNameIndex;
        private readonly int _jobCategoryNameMaxLength = 100;

        public override void Up()
        {
            CreateTable(
                this._tableName,
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: this._jobCategoryNameMaxLength)
                })
                .PrimaryKey(t => t.Id);
            CreateIndex(
                    table: this._tableName,
                    column: this._nameColumn,
                    unique: true,
                    name: this._uniqueNameIndex);

        }

        public override void Down()
        {
            DropTable(this._tableName);
        }
    }
}
