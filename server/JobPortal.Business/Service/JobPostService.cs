using AutoMapper;
using JobPortal.Business.Core.Contract;
using JobPortal.Business.Core.Model;
using JobPortal.Common.Model;
using JobPortal.DataAccess.Core.Contract;
using JobPortal.DataAccess.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Business.Service
{
    public class JobPostService : IJobPostService
    {
        private readonly int _defaultItemsPerPage = 10;
        private IMapper _modelMapper;
        private IJobPostRepository _jobPostRepository;

        public ServiceJobPostListResult GetJobPosts(ServiceJobPostSearchInputModel searchInputModel)
        {
            var result = new ServiceJobPostListResult();
            if (searchInputModel.ItemsPerPage <= 0)
            {
                searchInputModel.ItemsPerPage = this._defaultItemsPerPage;
            }
            if (searchInputModel.PageNumber <= 0)
            {
                searchInputModel.PageNumber = 1;
            }
            var itemsPerPage = searchInputModel.ItemsPerPage;
            var repositorySearchInputModel = this._modelMapper.Map<ServiceJobPostSearchInputModel, RepositoryJobPostSearchInputModel>(searchInputModel);
            repositorySearchInputModel.Take = itemsPerPage;
            repositorySearchInputModel.Skip = (searchInputModel.PageNumber - 1) * itemsPerPage;
            try
            {
                result.TotalCount = this._jobPostRepository.GetJobPostCount(repositorySearchInputModel);
                result.PageCount = (result.TotalCount + itemsPerPage - 1) / itemsPerPage;
                var jobPosts = this._jobPostRepository.GetJobPosts(repositorySearchInputModel);
                result.JobPosts = this._modelMapper.Map<IEnumerable<RepositoryJobPostResult>, IEnumerable<ServiceJobPostResult>>(jobPosts);
                result.HasFailed = false;
            }
            catch (Exception exc)
            {
                // Normally, should log the error using library like ELMAH
                result.HasFailed = true;
            }
            return result;
        }

        public JobPostService(
            IMapper modelMapper, 
            IJobPostRepository jobPostRepository
        )
        {
            this._modelMapper = modelMapper;
            this._jobPostRepository = jobPostRepository;
        }
    }
}
