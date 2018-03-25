import angular from 'angular';
import templateUrl from './job-listing-container.component.html';

class JobListingContainerController {
  constructor($stateParams, $state, JobPostService) {
    'ngInject';
    this.$stateParams = $stateParams;
    this.$state = $state;
    this.JobPostService = JobPostService;
    this.defaultItemsPerPage = 10;
    this.SORT_ORDER = {
      ByCreatedDate: 0,
      ByViews: 1
    };
  }

  changeSortOrder(newSortOrder) {
    this.$state.go('.', {
      SortOrder: newSortOrder
    });
    this.searchOptions.SortOrder = newSortOrder;
    this.loadJobPosts();
  }

  changePage(newPage) {
    this.$state.go('.', {
      PageNumber: newPage
    });
    this.searchOptions.PageNumber = newPage;
    this.loadJobPosts();
  }

  loadJobPosts() {
    this.jobListData.isLoading = true;
    this.JobPostService.getJobPosts(this.searchOptions)
      .then((result) => {
        if (result.success) {
          const {
            JobPosts,
            PageCount,
            TotalCount
          } = result.data;
          this.jobListData = {
            ...this.jobListData,
            jobPosts: JobPosts,
            pageCount: PageCount,
            totalCount: TotalCount,
            pageNumber: this.searchOptions.PageNumber,
          };
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

  getSearchOptionsFromUrl() {
    const searchOptions = {};
    if ((angular.isDefined(this.$stateParams.KeywordSearchType)) && (angular.isDefined(this.$stateParams.Keyword))) {
      searchOptions.KeywordSearchType = parseInt(this.$stateParams.KeywordSearchType, 10);
      searchOptions.Keyword = this.$stateParams.Keyword;
    }
    searchOptions.CategoryIds = this.parseIntegerArray(this.$stateParams.CategoryIds);
    searchOptions.EmploymentTypeIds = this.parseIntegerArray(this.$stateParams.EmploymentTypeIds);
    searchOptions.LocationIds = this.parseIntegerArray(this.$stateParams.LocationIds);
    searchOptions.SortOrder = this.getSortOrder();
    if (angular.isDefined(this.$stateParams.PageNumber)) {
      searchOptions.PageNumber = parseInt(this.$stateParams.PageNumber);
    } else {
      searchOptions.PageNumber = 1;
    }
    return searchOptions;
  }

  getSortOrder() {
    const sortOrder = this.$stateParams.SortOrder;
    if (sortOrder != null) {
      return parseInt(sortOrder, 10);
    } else {
      return this.SORT_ORDER.ByCreatedDate;
    }
  }

  $onInit() {
    this.isLoading = true;
    this.hasErrorOccurred = false;
    this.searchOptions = this.getSearchOptionsFromUrl();
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
            data : {
              JobPosts,
              PageCount,
              TotalCount,
              EmploymentTypes,
              JobCategories,
              Locations
            }
          } = result;
          this.jobListData = {
            ...this.jobListData,
            isLoading: false,
            jobPosts: JobPosts,
            pageCount: PageCount,
            totalCount: TotalCount,
            pageNumber: this.searchOptions.PageNumber,
          };
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
