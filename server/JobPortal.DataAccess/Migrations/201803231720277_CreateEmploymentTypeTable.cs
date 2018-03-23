namespace JobPortal.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateEmploymentTypeTable : DbMigration
    {
        private readonly string EmploymentTypeTableName = "dbo.EmploymentTypes";
        private readonly string NameColumn = "Name";
        private readonly string UniqueNameIndex = "UX_EmploymentTypeName";
        private readonly int EmploymentTypeNameMaxLength = 50;


        public override void Up()
        {
            CreateTable(
                this.EmploymentTypeTableName,
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: this.EmploymentTypeNameMaxLength)
                })
                .PrimaryKey(t => t.Id);
            CreateIndex(
                    table: this.EmploymentTypeTableName,
                    column: this.NameColumn,
                    unique: true,
                    name: this.UniqueNameIndex);

        }

        public override void Down()
        {
            DropIndex(this.EmploymentTypeTableName, this.UniqueNameIndex);
            DropTable(this.EmploymentTypeTableName);
        }
    }
}
