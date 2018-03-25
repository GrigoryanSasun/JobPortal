import angular from 'angular';
import { JobPostService } from './services/job-post.service';
import { JobListingContainerComponent } from './components/job-listing-container/job-listing-container.component';
import { JobListComponent } from './components/job-list/job-list.component';
import { SearchComponent } from './components/search/search.component';
import { CategoryFilterComponent } from './components/category-filter/category-filter.component';
import { EmploymentTypeFilterComponent } from './components/employment-type-filter/employment-type-filter.component';


const jobListingContainerComponentName = 'jobListingContainer';
const jobListComponentName = 'jobList';
const searchComponentName = 'search';
const categoryFilterComponentName = 'categoryFilter';
const employmentTypeFilterComponentName = 'employmentTypeFilter';

const jobListingUrl = '/jobs';

export const JobListingModule = angular
  .module('app.common.job-listing', [])
  .service('JobPostService', JobPostService)
  .component(jobListingContainerComponentName, JobListingContainerComponent)
  .component(jobListComponentName, JobListComponent)
  .component(searchComponentName, SearchComponent)
  .component(categoryFilterComponentName, CategoryFilterComponent)
  .component(employmentTypeFilterComponentName, EmploymentTypeFilterComponent)
  .config(($locationProvider, $stateProvider, $urlRouterProvider) => {
    'ngInject';
    $locationProvider.html5Mode(true);

    $stateProvider
      .state('jobs', {
        url: `${jobListingUrl}?KeywordSearchType&Keyword&CategoryIds&EmploymentTypeIds&LocationIds&SortOrder&PageNumber`,
        params: {
          SortOrder: {
            dynamic: true
          },
          PageNumber: {
            dynamic: true
          },
          KeywordSearchType: {
            dynamic: true
          },
          Keyword: {
            dynamic: true
          },
          CategoryIds: {
            dynamic: true,
            array: true,
          },
          EmploymentTypeIds: {
            dynamic: true,
            array: true,
          }
        },
        component: jobListingContainerComponentName,
      });
    $urlRouterProvider.otherwise(jobListingUrl);
  })
  .name;
