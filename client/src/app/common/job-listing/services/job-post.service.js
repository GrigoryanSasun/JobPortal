export class JobPostService {
  constructor($http) {
    'ngInject';
    this.$http = $http;
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
}
