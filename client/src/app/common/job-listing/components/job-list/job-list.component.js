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

  notifyChangePage() {
    this.onPageChanged({
      newPage: this.jobListData.pageNumber,
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
