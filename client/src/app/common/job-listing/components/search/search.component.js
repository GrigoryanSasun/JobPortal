import templateUrl from './search.component.html';

class SearchController {
  $onInit() {
    const {
      ByTitle,
      ByJobCategory,
      ByLocation,
    } = this.searchData.keywordSearchTypes;
    this.keywordSearchTypes = [
      {
        label: 'Job title',
        value: ByTitle
      },
      {
        label: 'Job category',
        value: ByJobCategory
      },
      {
        label: 'Location',
        value: ByLocation
      }
    ];
  }

  notifySearchOptionChange() {
    const newKeywordSearchType = this.searchData.currentKeywordSearchType;
    const newKeyword = this.searchData.keyword;
    this.onSearchOptionsChanged({
      newKeywordSearchType,
      newKeyword
    });
  }
}

export const SearchComponent = {
  templateUrl,
  controller: SearchController,
  bindings: {
    searchData: '<',
    onSearchOptionsChanged: '&'
  }
};
