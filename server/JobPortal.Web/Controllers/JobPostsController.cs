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
    [RoutePrefix("jobposts")]
    public class JobPostsController : ApiControllerBase
    {
        private IHttpActionResult SetBookmarkStatus(int jobPostId, bool isBookmarked)
        {
            bool hasSucceeded = this._jobPostService.SetBookmarkState(jobPostId, isBookmarked: isBookmarked);
            if (hasSucceeded)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        [Route("{*pathvalue}")]
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
        
        [Route("{jobPostId:int}/bookmark")]
        [HttpPut]
        public IHttpActionResult CreateBookmark(int jobPostId)
        {
            return SetBookmarkStatus(jobPostId, isBookmarked: true);
        }

        [Route("{jobPostId:int}/bookmark")]
        [HttpDelete]
        public IHttpActionResult DeleteBookmark(int jobPostId)
        {
            return SetBookmarkStatus(jobPostId, isBookmarked: false);
        }

        public JobPostsController(
            IMapper modelMapper,
            IJobPostService jobPostService
        ): base(modelMapper, jobPostService)
        {}
    }
}
