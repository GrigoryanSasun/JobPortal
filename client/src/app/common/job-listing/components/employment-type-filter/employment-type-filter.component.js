import advancedFilterViewBase from '../shared/advanced-filter-view-base.html';
import { AdvancedFilterControllerBase } from '../shared/advanced-filter-controller-base.controller';

class EmploymentTypeFilterController extends AdvancedFilterControllerBase {
  getItems() {
    if (angular.isDefined(this.employmentTypeFilterData) && angular.isDefined(this.employmentTypeFilterData.employmentTypes)) {
      return this.employmentTypeFilterData.employmentTypes;
    }
    return [];
  }

  getLabelPropertyName() {
    return "Name";
  }
}

export const EmploymentTypeFilterComponent = {
  templateUrl: advancedFilterViewBase,
  controller: EmploymentTypeFilterController,
  bindings: {
    employmentTypeFilterData: '<',
    onFilterChanged: '&'
  }
};
