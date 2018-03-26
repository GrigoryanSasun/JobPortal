import templateUrl from './app.component.html';
import './app.scss';

class AppController {
  constructor(SpinnerService) {
    'ngInject';
    this.SpinnerService = SpinnerService;
  }

  $onInit() {
    this.showSpinner = false;
    this.SpinnerService.addSpinnerVisibilityChangedCallback((isVisible) => {
      this.showSpinner = isVisible;
    });
  }
}

export const AppComponent = {
  templateUrl,
  controller: AppController
};
