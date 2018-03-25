using AutoMapper;
using JobPortal.Business.Core.Contract;
using JobPortal.Business.Core.Model;
using JobPortal.Web.Controllers.Base;
using JobPortal.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobPortal.Web.Controllers
{
    [RoutePrefix("api/filterdata")]
    public class FilterDataController : ApiControllerBase
    {
        [Route("{*pathvalue}")]
        public IHttpActionResult GetFilterData([FromUri]AppJobPostSearchInputModel searchInputModel)
        {
            var serviceInputModel = this._modelMapper.Map<AppJobPostSearchInputModel, ServiceJobPostSearchInputModel>(
                searchInputModel
            );
            var serviceJobPostFilterResult = this._jobPostService.GetJobPostsAndFilterData(serviceInputModel);
            if (serviceJobPostFilterResult.HasFailed)
            {
                return InternalServerError();
            }
            var appFilterDataResult = this._modelMapper.Map<ServiceJobPostFilterDataResult, AppJobPostFilterDataResult>(serviceJobPostFilterResult);
            return Ok(appFilterDataResult);
        }

        public FilterDataController(
            IMapper modelMapper,
            IJobPostService jobPostService
        ) : base(modelMapper, jobPostService)
        { }
    }
}
