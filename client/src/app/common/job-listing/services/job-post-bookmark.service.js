export class JobPostBookmarkService {
  constructor() {
    this.pendingBookmarkJobPostIdMap = {};
  }

  markJobPostBeingBookmarked(jobPost) {
    this.pendingBookmarkJobPostIdMap[jobPost.Id] = true;
  }

  markJobPostBookmarkingDone(jobPost) {
    delete this.pendingBookmarkJobPostIdMap[jobPost.Id];
  }

  syncIsBeingBookmarkedState(jobPost) {
    const isBeingBookmarked = !!this.pendingBookmarkJobPostIdMap[jobPost.Id];
    jobPost.isBeingBookmarked = isBeingBookmarked;
  }
}
