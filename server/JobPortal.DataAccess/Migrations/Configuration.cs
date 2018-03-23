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

        private void SeedJobCategories(JobPortalDbContext context)
        {
            context.JobCategories.AddOrUpdate(
                jc => jc.Name,
                new JobCategory { Name = "Software development" },
                new JobCategory { Name = "Quality assurance" },
                new JobCategory { Name = "Web / Graphic design" },
                new JobCategory { Name = "Product / Project management" },
                new JobCategory { Name = "Other IT / tech" });
        }

        protected override void Seed(JobPortalDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            this.SeedEmploymentTypes(context);
            this.SeedJobCategories(context);
            context.SaveChanges();
        }
    }
}
