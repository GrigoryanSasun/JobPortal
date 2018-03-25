using AutoMapper;
using JobPortal.Business.Core.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace JobPortal.Web.Controllers.Base
{
    public class ApiControllerBase: ApiController
    {
        protected IMapper _modelMapper;
        protected IJobPostService _jobPostService;

        public ApiControllerBase(
            IMapper modelMapper,
            IJobPostService jobPostService
        )
        {
            this._modelMapper = modelMapper;
            this._jobPostService = jobPostService;
        }

    }
}