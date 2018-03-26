import moment from 'moment';

export class JobPostService {
  constructor($http, JobPostBookmarkService) {
    'ngInject';
    this.$http = $http;
    this.JobPostBookmarkService = JobPostBookmarkService;
  }

  getJobPostData(endpoint, searchOptions) {
    const result = {
      success: false,
    };
    return this.$http.get(endpoint, {
      method: 'GET',
      params: searchOptions
    }).then((response) => {
      const { data } = response;
      const {
        JobPosts
      } = data;
      this.jobPostIdMap = {};
      // Syncs the post bookmarked state for all the job posts
      for (let i = 0; i < JobPosts.length; i++) {
        const jobPost = JobPosts[i];
        jobPost.CreatedAt = moment(jobPost.CreatedAt).format('DD/MM/YYYY hh:mm');
        this.jobPostIdMap[jobPost.Id] = jobPost;
        this.JobPostBookmarkService.syncIsBeingBookmarkedState(jobPost);
      }
      result.success = true;
      result.data = data;
      return result;
    }).catch((response) => {
      return result;
    });
  }

  getJobPosts(searchOptions) {
    return this.getJobPostData('/api/jobposts', searchOptions);
  }

  getFilterDataAndJobPosts(searchOptions) {
    return this.getJobPostData('/api/filterdata', searchOptions);
  }

  bookmarkJobPost(jobPost, shouldRemoveBookmark) {
    const httpMethod = shouldRemoveBookmark ? 'delete' : 'put';
    const result = {
      success: false,
    };
    jobPost.isBeingBookmarked = true;
    this.JobPostBookmarkService.markJobPostBeingBookmarked(jobPost);
    const jobPostId = jobPost.Id;
    return this.$http[httpMethod](`/api/jobposts/${jobPostId}/bookmark`)
      .then(() => {
        result.success = true;
        const currentJobPost = this.jobPostIdMap[jobPostId];
        if (currentJobPost) {
          currentJobPost.IsBookmarked = !jobPost.IsBookmarked;
        }
        return result;
      }).catch(() => {
        return result;
      }).finally(() => {
        const currentJobPost = this.jobPostIdMap[jobPostId];
        if (currentJobPost) {
          currentJobPost.isBeingBookmarked = false;
        }
        this.JobPostBookmarkService.markJobPostBookmarkingDone(jobPost);
      });
  }
}
