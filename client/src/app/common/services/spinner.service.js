export class SpinnerService {
  constructor() {
    this._onSpinnerVisibilityChangedCallbacks = [];
  }

  addSpinnerVisibilityChangedCallback(callback) {
    if (typeof (callback) !== 'function') {
      throw new TypeError('The provided argument should be a function');
    }
    this._onSpinnerVisibilityChangedCallbacks.push(callback);
  }

  setSpinnerVisibility(isVisible) {
    for (let i = 0; i < this._onSpinnerVisibilityChangedCallbacks.length; i++) {
      const callback = this._onSpinnerVisibilityChangedCallbacks[i];
      callback(isVisible);
    }
  }
}
