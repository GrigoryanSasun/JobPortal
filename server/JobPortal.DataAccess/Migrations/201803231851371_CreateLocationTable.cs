namespace JobPortal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLocationTable : DbMigration
    {
        private readonly string _tableName = DbHelpers.GetTableNameWithSchema(DbHelpers.LocationTableName);
        private readonly string _addressColumn = "Address";
        private readonly string _uniqueAddressIndex = "UX_Address";
        private readonly int _addressMaxLength = 300;

        public override void Up()
        {
            CreateTable(
                this._tableName,
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Address = c.String(nullable: false, maxLength: this._addressMaxLength)
                })
                .PrimaryKey(t => t.Id);
            CreateIndex(
                    table: this._tableName,
                    column: this._addressColumn,
                    unique: true,
                    name: this._uniqueAddressIndex);

        }

        public override void Down()
        {
            DropIndex(this._tableName, this._uniqueAddressIndex);
            DropTable(this._tableName);
        }
    }
}
