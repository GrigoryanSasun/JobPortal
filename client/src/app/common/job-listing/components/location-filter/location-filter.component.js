import advancedFilterViewBase from '../shared/advanced-filter-view-base.html';
import { AdvancedFilterControllerBase } from '../shared/advanced-filter-controller-base.controller';

class LocationFilterController extends AdvancedFilterControllerBase {
  getItems() {
    if (angular.isDefined(this.locationFilterData) && angular.isDefined(this.locationFilterData.locations)) {
      return this.locationFilterData.locations;
    }
    return [];
  }

  getLabelPropertyName() {
    return "Address";
  }
}

export const LocationFilterComponent = {
  templateUrl: advancedFilterViewBase,
  controller: LocationFilterController,
  bindings: {
    locationFilterData: '<',
    onFilterChanged: '&'
  }
};
