"use strict";

(function initializeHelpers(window) {
  function query(selector, base) {
    return Array.from((base !== null && base !== void 0 ? base : window.document).querySelectorAll(selector));
  }

  function ready(callback) {
    return document.addEventListener('DOMContentLoaded', function () {
      return callback(query);
    }, false);
  }

  window.helpers = {
    ready: ready,
    query: query
  };
})(window);