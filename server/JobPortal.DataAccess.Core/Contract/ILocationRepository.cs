using JobPortal.DataAccess.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Core.Contract
{
    public interface ILocationRepository
    {
        IEnumerable<RepositoryLocationResult> GetLocations();
    }
}
