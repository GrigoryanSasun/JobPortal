using AutoMapper;
using JobPortal.Business.Core.Model;
using JobPortal.DataAccess.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.Web.App_Start.AutoMapperProfiles
{
    public class BusinessDataAccessProfile: Profile
    {
        public BusinessDataAccessProfile()
        {
            CreateMap<ServiceJobPostSearchInputModel, RepositoryJobPostSearchInputModel>();
            CreateMap<RepositoryJobPostResult, ServiceJobPostResult>();
            CreateMap<RepositoryCategoryResult, ServiceCategoryResult>();
            CreateMap<RepositoryEmploymentTypeResult, ServiceEmploymentTypeResult>();
            CreateMap<RepositoryLocationResult, ServiceLocationResult>();
            CreateMap<ServiceJobPostListResult, ServiceJobPostFilterDataResult>();
        }
    }
}