namespace JobPortal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateJobTitleTable : DbMigration
    {
        private readonly string _tableName = DbHelpers.GetTableNameWithSchema(DbHelpers.JobTitleTableName);
        private readonly string _nameColumn = "Name";
        private readonly string _uniqueNameIndex = DbHelpers.GetIndexPrefixedName("JobTitleName", isUnique: true);
        private readonly int _jobTitleNameMaxLength = 100;

        public override void Up()
        {
            CreateTable(
                this._tableName,
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: this._jobTitleNameMaxLength)
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
