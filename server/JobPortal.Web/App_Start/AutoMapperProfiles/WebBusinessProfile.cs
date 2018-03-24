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
            CreateMap<ServiceJobPostListResult, AppJobPostListResult>();
        }
    }
}