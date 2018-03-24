using AutoMapper;
using JobPortal.Business.Core.Contract;
using JobPortal.Business.Core.Model;
using JobPortal.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobPortal.Web.Controllers
{
    public class JobPostsController : ApiController
    {
        private IMapper _modelMapper;
        private IJobPostService _jobPostService;

        public IHttpActionResult GetJobPosts([FromUri] AppJobPostSearchInputModel searchInputModel)
        {
            var serviceInputModel = this._modelMapper.Map<AppJobPostSearchInputModel, ServiceJobPostSearchInputModel>(
                searchInputModel
            );
            var serviceJobPostListResult = this._jobPostService.GetJobPosts(serviceInputModel);
            if (serviceJobPostListResult.HasFailed)
            {
                return InternalServerError();
            }
            var appJobPostListResult = this._modelMapper.Map<ServiceJobPostListResult, AppJobPostListResult>(serviceJobPostListResult);
            return Ok(appJobPostListResult);
        }

        public JobPostsController(
            IMapper modelMapper,
            IJobPostService jobPostService
        )
        {
            this._modelMapper = modelMapper;
            this._jobPostService = jobPostService;
        }
    }
}
