import angular from 'angular';

import { JobPostService } from './services/job-post.service';
import { JobPostBookmarkService } from './services/job-post-bookmark.service';

import { JobListingContainerComponent } from './components/job-listing-container/job-listing-container.component';
import { JobListComponent } from './components/job-list/job-list.component';
import { SearchComponent } from './components/search/search.component';
import { CategoryFilterComponent } from './components/category-filter/category-filter.component';
import { EmploymentTypeFilterComponent } from './components/employment-type-filter/employment-type-filter.component';
import { LocationFilterComponent } from './components/location-filter/location-filter.component';


const jobListingContainerComponentName = 'jobListingContainer';
const jobListComponentName = 'jobList';
const searchComponentName = 'search';
const categoryFilterComponentName = 'categoryFilter';
const employmentTypeFilterComponentName = 'employmentTypeFilter';
const locationFilterComponentName = 'locationFilter';

const jobListingUrl = '/jobs';

export const JobListingModule = angular
  .module('app.common.job-listing', [])
  .service('JobPostService', JobPostService)
  .service('JobPostBookmarkService', JobPostBookmarkService)
  .component(jobListingContainerComponentName, JobListingContainerComponent)
  .component(jobListComponentName, JobListComponent)
  .component(searchComponentName, SearchComponent)
  .component(categoryFilterComponentName, CategoryFilterComponent)
  .component(employmentTypeFilterComponentName, EmploymentTypeFilterComponent)
  .component(locationFilterComponentName, LocationFilterComponent)
  .config(($locationProvider, $stateProvider, $urlRouterProvider) => {
    'ngInject';
    $locationProvider.html5Mode(true);

    const dynamicParamDefinition = {
      dynamic: true
    };

    const dynamicArrayParamDefinition = {
      ...dynamicParamDefinition,
      array: true,
    };

    $stateProvider
      .state('jobs', {
        url: `${jobListingUrl}?KeywordSearchType&Keyword&CategoryIds&EmploymentTypeIds&LocationIds&SortOrder&PageNumber`,
        params: {
          SortOrder: dynamicParamDefinition,
          PageNumber: dynamicParamDefinition,
          KeywordSearchType: dynamicParamDefinition,
          Keyword: dynamicParamDefinition,
          CategoryIds: dynamicArrayParamDefinition,
          EmploymentTypeIds: dynamicArrayParamDefinition,
          LocationIds: dynamicArrayParamDefinition
        },
        component: jobListingContainerComponentName,
      });
    $urlRouterProvider.otherwise(jobListingUrl);
  })
  .name;
