import advancedFilterViewBase from '../shared/advanced-filter-view-base.html';
import { AdvancedFilterControllerBase } from '../shared/advanced-filter-controller-base.controller';

class CategoryFilterController extends AdvancedFilterControllerBase {
  getItems() {
    if (angular.isDefined(this.categoryFilterData) && angular.isDefined(this.categoryFilterData.categories)) {
      return this.categoryFilterData.categories;
    }
    return [];
  }

  getLabelPropertyName() {
    return "Name";
  }
}

export const CategoryFilterComponent = {
  templateUrl: advancedFilterViewBase,
  controller: CategoryFilterController,
  bindings: {
    categoryFilterData: '<',
    onFilterChanged: '&'
  }
};
