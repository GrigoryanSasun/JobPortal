import angular from 'angular';
import { JobPostService } from './services/job-post.service';
import { JobListingContainerComponent } from './components/job-listing-container/job-listing-container.component';
import { JobListComponent } from './components/job-list/job-list.component';

const JobListingContainerComponentName = 'jobListingContainer';
const JobListComponentName = 'jobList';
const jobListingUrl = '/jobs';

export const JobListingModule = angular
  .module('app.common.job-listing', [])
  .service('JobPostService', JobPostService)
  .component(JobListingContainerComponentName, JobListingContainerComponent)
  .component(JobListComponentName, JobListComponent)
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
        },
        component: JobListingContainerComponentName,
      });
    $urlRouterProvider.otherwise(jobListingUrl);
  })
  .name;
