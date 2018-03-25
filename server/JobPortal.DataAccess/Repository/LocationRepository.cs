using JobPortal.Context;
using JobPortal.DataAccess.Core.Contract;
using JobPortal.DataAccess.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Repository
{
    public class LocationRepository : ILocationRepository
    {
        public IEnumerable<RepositoryLocationResult> GetLocations()
        {
            using (var dbContext = new JobPortalDbContext())
            {
                return dbContext.Locations.OrderBy(l => l.Id).Select((l) => new RepositoryLocationResult
                {
                    Id = l.Id,
                    Address = l.Address
                }).ToList();
            }
        }
    }
}
