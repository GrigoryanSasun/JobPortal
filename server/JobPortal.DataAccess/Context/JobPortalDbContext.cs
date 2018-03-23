﻿using JobPortal.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Context
{
    class JobPortalDbContext : DbContext
    {
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }

        public JobPortalDbContext(): base("DbConnectionString")
        {

        }
    }
}