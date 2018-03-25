using JobPortal.Business.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Business.Core.Contract
{
    public interface IJobPostService
    {
        ServiceJobPostFilterDataResult GetJobPostsAndFilterData(ServiceJobPostSearchInputModel searchInputModel);
        ServiceJobPostListResult GetJobPosts(ServiceJobPostSearchInputModel searchInputModel);
        bool SetBookmarkState(int jobPostId, bool isBookmarked);
    }
}
