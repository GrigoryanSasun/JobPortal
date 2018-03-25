using AutoMapper;
using JobPortal.Business.Core.Model;
using JobPortal.Web.Model;

namespace JobPortal.Web.App_Start.AutoMapperProfiles
{
    public class WebBusinessProfile : Profile
    {
        public WebBusinessProfile()
        {
            CreateMap<AppJobPostSearchInputModel, ServiceJobPostSearchInputModel>();
            CreateMap<ServiceJobPostResult, AppJobPostResult>();
            CreateMap<ServiceCategoryResult, AppJobCategoryResult>();
            CreateMap<ServiceEmploymentTypeResult, AppEmploymentTypeResult>();
            CreateMap<ServiceLocationResult, AppLocationResult>();
            CreateMap<ServiceJobPostListResult, AppJobPostListResult>();
            CreateMap<ServiceJobPostFilterDataResult, AppJobPostFilterDataResult>();
        }
    }
}