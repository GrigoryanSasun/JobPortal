import angular from 'angular';
import templateUrl from './job-listing-container.component.html';

import './job-listing-container.component.scss';

class JobListingContainerController {
  constructor($stateParams, $state, $transitions, SpinnerService, JobPostService) {
    'ngInject';
    this.$transitions = $transitions;
    this.$stateParams = $stateParams;
    this.$state = $state;
    this.JobPostService = JobPostService;
    this.SpinnerService = SpinnerService;
    this.SORT_ORDER = {
      ByCreatedDate: 0,
      ByViews: 1
    };
    this.KEYWORD_SEARCH_TYPES = {
      ByTitle: 0,
      ByJobCategory: 1,
      ByLocation: 2
    };
    this.showOnlyOneAdvancedFilter = false;
    this.categoriesFilterOpen = true;
    this.employmentTypesFilterOpen = true;
    this.locationsFilterOpen = true;
  }

  bookmark(jobPost, shouldRemoveBookmark) {
    this.JobPostService.bookmarkJobPost(jobPost, shouldRemoveBookmark)
      .then((result) => {
        if (result.success) {
          //TODO: show toastr success notification
        } else {
          //TODO: show toastr failure notification
        }
      });
  }

  changeQueryParams(newParams) {
    this.$state.go('.', newParams);
  }

  changeSortOrder(newSortOrder) {
    this.changeQueryParams({
      SortOrder: newSortOrder
    });
  }

  changeSearchOptions(newKeywordSearchType, newKeyword) {
    this.changeQueryParams({
      KeywordSearchType: newKeywordSearchType,
      Keyword: newKeyword,
      PageNumber: 1,
    });
  }

  changeCategoryIds(selectedCategoryIds) {
    this.changeQueryParams({
      CategoryIds: selectedCategoryIds,
      PageNumber: 1,
    });
  }

  changeEmploymentTypeIds(employmentTypeIds) {
    this.changeQueryParams({
      EmploymentTypeIds: employmentTypeIds,
      PageNumber: 1,
    });
  }

  changeLocationIds(locationIds) {
    this.changeQueryParams({
      LocationIds: locationIds,
      PageNumber: 1,
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

  updateSearchData() {
    this.searchData = {
      keywordSearchTypes: this.KEYWORD_SEARCH_TYPES,
      currentKeywordSearchType: this.searchOptions.KeywordSearchType,
      keyword: this.searchOptions.Keyword
    };
  }

  updateAdvancedFilterData(searchParamPath, actualDataPath, advancedFilterDataPath, advancedFilterItemsPath) {
    const itemIdMap = {};
    const itemIds = this.searchOptions[searchParamPath];
    for (let i = 0; i < itemIds.length; i++) {
      const itemId = itemIds[i];
      itemIdMap[itemId] = true;
    }
    if (this[actualDataPath]) {
      for (let i = 0; i < this[actualDataPath].length; i++) {
        const item = this[actualDataPath][i];
        item.isSelected = !!itemIdMap[item.Id];
      }
    }
    this[advancedFilterDataPath] = {
      [advancedFilterItemsPath]: this[actualDataPath]
    };
  }

  updateCategoryFilterData() {
    this.updateAdvancedFilterData('CategoryIds', 'jobCategories', 'categoryFilterData', 'categories');
  }

  updateEmploymentTypeFilterData() {
    this.updateAdvancedFilterData('EmploymentTypeIds', 'employmentTypes', 'employmentTypeFilterData', 'employmentTypes');
  }

  updateLocationFilterData() {
    this.updateAdvancedFilterData('LocationIds', 'locations', 'locationFilterData', 'locations');
  }

  updateAdvancedFilters() {
    this.updateCategoryFilterData();
    this.updateEmploymentTypeFilterData();
    this.updateLocationFilterData();
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
    } else {
      searchOptions.KeywordSearchType = this.KEYWORD_SEARCH_TYPES.ByTitle;
      searchOptions.Keyword = '';
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
      this.updateSearchData();
      this.updateAdvancedFilters();
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
    this.updateSearchData();
    this.SpinnerService.setSpinnerVisibility(true);
    this.JobPostService.getFilterDataAndJobPosts(this.searchOptions)
      .then((result) => {
        if (result.success) {
          const {
            data: {
              JobPosts,
              EmploymentTypes,
              JobCategories,
              Locations
            }
          } = result;
          this.updateJobListData(result.data);
          this.employmentTypes = EmploymentTypes;
          this.jobCategories = JobCategories;
          this.locations = Locations;
          this.jobPosts = JobPosts;
          this.updateAdvancedFilters();
        } else {
          this.hasErrorOccurred = true;
        }
        this.isLoading = false;
        this.SpinnerService.setSpinnerVisibility(false);
      });
  }
}

export const JobListingContainerComponent = {
  templateUrl,
  controller: JobListingContainerController,
};
