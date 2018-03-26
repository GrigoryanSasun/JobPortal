import templateUrl from './advanced-filter-accordion-group.component.html';

export const AdvancedFilterAccordionGroup = {
  templateUrl,
  transclude: true,
  bindings: {
    isOpen: '<',
    title: '@'
  }
};
