using JobPortal.Context;
using JobPortal.DataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess
{
    public static class DataAccessSetup
    {
        public static void SetupDataAccess()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JobPortalDbContext, Configuration>());
        }
    }
}
