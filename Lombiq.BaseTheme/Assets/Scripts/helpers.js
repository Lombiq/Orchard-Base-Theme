(function initializeHelpers(window) {
    function query(selector, base) {
        return Array.from((base ?? window.document).querySelectorAll(selector));
    }

    function ready(callback) {
        return document.addEventListener('DOMContentLoaded', () => callback(query), false);
    }

    window.helpers = { ready, query };
})(window);
