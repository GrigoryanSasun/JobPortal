namespace JobPortal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateJobCategoryTable : DbMigration
    {
        private readonly string _tableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobCategoryTableName);
        private readonly string _nameColumn = "Name";
        private readonly string _uniqueNameIndex = "UX_JobCategoryName";
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
            DropIndex(this._tableName, this._uniqueNameIndex);
            DropTable(this._tableName);
        }
    }
}
