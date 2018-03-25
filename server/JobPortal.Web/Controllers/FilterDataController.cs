using JobPortal.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobPortal.Web.Controllers
{
    public class FilterDataController : ApiController
    {
        public IHttpActionResult GetFilterData([FromUri]AppJobPostSearchInputModel inputModel)
        {

        }
    }
}
