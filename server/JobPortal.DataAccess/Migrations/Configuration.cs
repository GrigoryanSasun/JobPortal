namespace JobPortal.DataAccess.Migrations
{
    using JobPortal.Context;
    using JobPortal.DataAccess.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobPortalDbContext>
    {
        private readonly int _jobPostSeedCount = 25;

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

        private void SeedJobTitles(JobPortalDbContext context)
        {
            context.JobTitles.AddOrUpdate(
                jt => jt.Name,
                new JobTitle { Name = "Senior Web Developer" },
                new JobTitle { Name = "Software Engineer" },
                new JobTitle { Name = "Senior Full-stack developer" },
                new JobTitle { Name = "Junior QA Engineer" },
                new JobTitle { Name = "CTO" });
        }

        private void SeedLocations(JobPortalDbContext context)
        {
            context.Locations.AddOrUpdate(
                l => l.Address,
                new Location { Address = "Yerevan, Armenia" },
                new Location { Address = "San Francisco, CA, US" },
                new Location { Address = "Chester, CA, US" },
                new Location { Address = "Birmingham, IA, US" }
            );
        }

        private void SeedJobPosts(JobPortalDbContext context)
        {
            var jobPostCount = context.JobPosts.Count();
            var missingJobPostCount = this._jobPostSeedCount - jobPostCount;
            if (missingJobPostCount > 0)
            {
                var rnd = new Random();
                var employmentTypes = context.EmploymentTypes.ToList();
                var jobCategories = context.JobCategories.ToList();
                var jobTitles = context.JobTitles.ToList();
                var locations = context.Locations.ToList();
                var imageUrls = new string[]
                {
                    "https://i.forbesimg.com/media/lists/companies/dell_416x416.jpg",
                    "https://acclaim-production-app.s3.amazonaws.com/images/854d76bf-4f74-4d51-98a0-d969214bfba7/large_IBM%2BLogo%2Bfor%2BAcclaim%2BProfile.png",
                    "https://s3-eu-west-1.amazonaws.com/jobbio-production/topic/benivo-836047844-logo.jpg"
                };
                for (int i = 0; i < missingJobPostCount; i++)
                {
                    var employmentIndex = rnd.Next(0, employmentTypes.Count);
                    var jobCategoryIndex = rnd.Next(0, jobCategories.Count);
                    var jobTitleIndex = rnd.Next(0, jobTitles.Count);
                    var locationIndex = rnd.Next(0, locations.Count);
                    var randomDayCount = rnd.Next(0, 10);
                    var randomViewCount = rnd.Next(0, 1001);
                    var randomImageIndex = rnd.Next(0, imageUrls.Length);
                    var jobPost = new JobPost
                    {
                        EmploymentTypeId = employmentTypes[employmentIndex].Id,
                        CategoryId = jobCategories[jobCategoryIndex].Id,
                        TitleId = jobTitles[jobTitleIndex].Id,
                        LocationId = locations[locationIndex].Id,
                        IsBookmarked = false,
                        Views = randomViewCount,
                        JobPostThumbnailUrl = imageUrls[randomImageIndex],
                        CreatedAt = DateTime.UtcNow.AddDays(randomDayCount)
                    };
                    context.JobPosts.Add(jobPost);
                }
                context.SaveChanges();
            }
        }

        protected override void Seed(JobPortalDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            this.SeedEmploymentTypes(context);
            this.SeedJobCategories(context);
            this.SeedJobTitles(context);
            this.SeedLocations(context);
            context.SaveChanges();

            this.SeedJobPosts(context);
            context.SaveChanges();
        }
    }
}
