import angular from 'angular';
import { JobListingModule } from './job-listing/job-listing.module';
import { SpinnerService } from './services/spinner.service';

export const CommonModule = angular
  .module('app.common', [
    JobListingModule
  ])
  .service('SpinnerService', SpinnerService)
  .name;
