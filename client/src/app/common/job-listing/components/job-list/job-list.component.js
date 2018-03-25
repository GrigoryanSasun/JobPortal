import templateUrl from './job-list.component.html';

class JobListController {
  constructor() {
    'ngInject';
  }

  $onInit() {
    const {
      ByCreatedDate,
      ByViews
    } = this.jobListData.sortOrderValues;
    this.pages = [];
    this.sortOrderOptions = [
      {
        label: 'Most recent',
        value: ByCreatedDate
      },
      {
        label: 'Most viewed',
        value: ByViews
      }
    ];
  }

  // TODO: remove when pagination component is added
  $onChanges(changesObj) {
    if (changesObj.jobListData && changesObj.jobListData.currentValue.pageCount){
      this.pages = [];
      for (let i = 0; i < changesObj.jobListData.currentValue.pageCount; i++) {
        this.pages.push(i + 1);
      }
    }
  }


  notifySortOrderChange() {
    const newSortOrder = this.jobListData.currentSortOrder;
    this.onSortOrderChange({
      newSortOrder,
    });
  }

  notifyChangePage(newPage) {
    this.onPageChanged({
      newPage,
    });
  }
}

export const JobListComponent = {
  templateUrl,
  controller: JobListController,
  bindings: {
    jobListData: '<',
    onSortOrderChange: '&',
    onPageChanged: '&'
  }
};
