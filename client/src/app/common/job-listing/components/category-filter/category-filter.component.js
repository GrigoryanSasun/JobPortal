import templateUrl from './category-filter.component.html';

class CategoryFilterController {
  notifyCategoryFilterChange() {
    const categories = this.categoryFilterData.categories;
    const selectedCategoryIds = [];
    for (let i = 0; i < categories.length; i++) {
      const category = categories[i];
      if (category.isSelected) {
        selectedCategoryIds.push(category.Id);
      }
    }
    this.onCategoryFilterChanged({
      selectedCategoryIds,
    });
  }
}

export const CategoryFilterComponent = {
  templateUrl,
  controller: CategoryFilterController,
  bindings: {
    categoryFilterData: '<',
    onCategoryFilterChanged: '&'
  }
};
