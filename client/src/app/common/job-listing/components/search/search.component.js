import templateUrl from './search.component.html';
import './search.component.scss';

class SearchController {
  $onInit() {
    const {
      ByTitle,
      ByJobCategory,
      ByLocation,
    } = this.searchData.keywordSearchTypes;
    const jobTitleType = {
      label: 'Job title',
      value: ByTitle
    };
    const jobCategoryType = {
      label: 'Job category',
      value: ByJobCategory
    };
    const locationType = {
      label: 'Location',
      value: ByLocation
    };
    this.keywordSearchTypes = [
      jobTitleType,
      jobCategoryType,
      locationType
    ];

    this.keywordSearchTypeMap = {
      [ByTitle]: jobTitleType,
      [ByJobCategory]: jobCategoryType,
      [ByLocation]: locationType
    }
  }

  changeKeywordSearchType(newKeywordSearchType) {
    this.searchData.currentKeywordSearchType = newKeywordSearchType;
  }

  getCurrentKeywordSearchTypeLabel() {
    const type =  this.keywordSearchTypeMap[this.searchData.currentKeywordSearchType];
    return type.label;
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
