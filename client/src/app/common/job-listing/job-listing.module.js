import angular from 'angular';
import { JobListingContainerComponent } from './components/job-listing-container/job-listing-container.component';

export const JobListingModule = angular
  .module('app.common.job-listing', [])
  .component('jobListingContainer', JobListingContainerComponent)
  .config(($locationProvider, $stateProvider, $urlRouterProvider) => {
    'ngInject';
    $locationProvider.html5Mode(true);

    $stateProvider
      .state('jobs', {
        url: '/jobs',
        component: 'jobListingContainer'
      });
    $urlRouterProvider.otherwise('/jobs');
  })
  .name;
