namespace JobPortal.DataAccess.Migrations
{
    using JobPortal.Context;
    using JobPortal.DataAccess.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobPortalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private void SeedEmploymentTypes(JobPortalDbContext context)
        {
            context.EmploymentTypes.AddOrUpdate(
                et => et.Name,
                new EmploymentType { Name = "Full Time" },
                new EmploymentType { Name = "Part Time" },
                new EmploymentType { Name = "Contractor" },
                new EmploymentType { Name = "Intern" },
                new EmploymentType { Name = "Seasonal / Temp" });
        }

        protected override void Seed(JobPortalDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            this.SeedEmploymentTypes(context);
            context.SaveChanges();
        }
    }
}
