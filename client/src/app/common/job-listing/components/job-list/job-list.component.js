import templateUrl from './job-list.component.html';
import './job-list.component.scss';

class JobListController {
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

  bookmarkJobPost(jobPost) {
    const shouldRemoveBookmark = jobPost.IsBookmarked;
    this.onBookmarkRequested({
      jobPost,
      shouldRemoveBookmark,
    });
  }


  notifySortOrderChange() {
    const newSortOrder = this.jobListData.currentSortOrder;
    this.onSortOrderChanged({
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
    onSortOrderChanged: '&',
    onPageChanged: '&',
    onBookmarkRequested: '&'
  }
};
