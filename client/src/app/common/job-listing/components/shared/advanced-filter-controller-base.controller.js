export class AdvancedFilterControllerBase {
  notifyFilterChange() {
    const items = this.getItems();
    const selectedItemIds = [];
    for (let i = 0; i < items.length; i++) {
      const item = items[i];
      if (item.isSelected) {
        selectedItemIds.push(item.Id);
      }
    }
    this.onFilterChanged({
      selectedItemIds,
    });
  }

  constructor() {
    if (typeof (this.getItems) !== 'function') {
      throw new TypeError("Must provide the method getItems");
    }
    if (typeof(this.getLabelPropertyName) !== 'function') {
      throw new TypeError("Must provide the method getLabelPropertyName");
    }
  }
}
