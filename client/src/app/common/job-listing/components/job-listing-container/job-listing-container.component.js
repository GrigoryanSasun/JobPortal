import angular from 'angular';
import templateUrl from './job-listing-container.component.html';

class JobListingContainerController {
  constructor($stateParams, $state, $transitions, JobPostService) {
    'ngInject';
    this.$transitions = $transitions;
    this.$stateParams = $stateParams;
    this.$state = $state;
    this.JobPostService = JobPostService;
    this.SORT_ORDER = {
      ByCreatedDate: 0,
      ByViews: 1
    };
  }

  changeQueryParams(newParams) {
    this.$state.go('.', newParams);
  }

  changeSortOrder(newSortOrder) {
    this.changeQueryParams({
      SortOrder: newSortOrder
    });
  }

  changePage(newPage) {
    this.changeQueryParams({
      PageNumber: newPage
    });
  }

  updateJobListData(newData) {
    const {
      JobPosts,
      PageCount,
      TotalCount
    } = newData;
    this.jobListData = {
      ...this.jobListData,
      isLoading: false,
      jobPosts: JobPosts,
      pageCount: PageCount,
      totalCount: TotalCount,
      pageNumber: this.searchOptions.PageNumber,
      currentSortOrder: this.searchOptions.SortOrder,
    };
  }

  loadJobPosts() {
    this.jobListData.isLoading = true;
    this.JobPostService.getJobPosts(this.searchOptions)
      .then((result) => {
        if (result.success) {
          this.updateJobListData(result.data);
        }
        this.jobListData.isLoading = false;
      });
  }

  parseIntegerArray(array) {
    if (angular.isDefined(array)) {
      return array.map((item) => {
        return parseInt(item, 10);
      });
    }
    return [];
  }

  getSearchOptionsFromParams(stateParams) {
    const searchOptions = {};
    if ((angular.isDefined(stateParams.KeywordSearchType)) && (angular.isDefined(stateParams.Keyword))) {
      searchOptions.KeywordSearchType = parseInt(stateParams.KeywordSearchType, 10);
      searchOptions.Keyword = stateParams.Keyword;
    }
    searchOptions.CategoryIds = this.parseIntegerArray(stateParams.CategoryIds);
    searchOptions.EmploymentTypeIds = this.parseIntegerArray(stateParams.EmploymentTypeIds);
    searchOptions.LocationIds = this.parseIntegerArray(stateParams.LocationIds);
    searchOptions.SortOrder = this.getSortOrder(stateParams);
    if (angular.isDefined(stateParams.PageNumber)) {
      searchOptions.PageNumber = parseInt(stateParams.PageNumber);
    } else {
      searchOptions.PageNumber = 1;
    }
    return searchOptions;
  }

  getSortOrder(stateParams) {
    const sortOrder = stateParams.SortOrder;
    if (angular.isDefined(sortOrder)) {
      return parseInt(sortOrder, 10);
    } else {
      return this.SORT_ORDER.ByCreatedDate;
    }
  }

  $onInit() {
    this.$transitions.onSuccess({ to: 'jobs' }, (transition) => {
      // Happened as a result of url change, should reload the data
      this.searchOptions = this.getSearchOptionsFromParams(transition.params('to'));
      this.loadJobPosts();
    });
    this.isLoading = true;
    this.hasErrorOccurred = false;
    this.searchOptions = this.getSearchOptionsFromParams(this.$stateParams);
    const SORT_ORDER = this.SORT_ORDER;
    this.jobListData = {
      isLoading: true,
      sortOrderValues: SORT_ORDER,
      currentSortOrder: this.searchOptions.SortOrder
    };
    this.JobPostService.getFilterDataAndJobPosts(this.searchOptions)
      .then((result) => {
        if (result.success) {
          const {
            data: {
              EmploymentTypes,
              JobCategories,
              Locations
            }
          } = result;
          this.updateJobListData(result.data);
          this.employmentTypes = EmploymentTypes;
          this.jobCategories = JobCategories;
          this.locations = Locations;
        } else {
          this.hasErrorOccurred = true;
        }
        this.isLoading = false;
      });
  }
}

export const JobListingContainerComponent = {
  templateUrl,
  controller: JobListingContainerController,
};
