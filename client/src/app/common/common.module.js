import angular from 'angular';
import { JobListingModule } from './job-listing/job-listing.module';
import { SpinnerService } from './services/spinner.service';

import { ThemeSettingsComponent } from './components/theme-settings.component';

const themeSettingsComponentName = 'themeSettings';

export const CommonModule = angular
  .module('app.common', [
    JobListingModule
  ])
  .component(themeSettingsComponentName, ThemeSettingsComponent)
  .service('SpinnerService', SpinnerService)
  .name;
