import angular from 'angular';
import { JobListingModule } from './job-listing/job-listing.module';

export const CommonModule = angular
  .module('app.common', [
    JobListingModule
  ])
  .name;
