(function initializeHelpers(window) {
    function query(selector, base) {
        const node = base ? base : window.document;
        return Array.from(node.querySelectorAll(selector));
    }

    function ready(callback) {
        return document.addEventListener('DOMContentLoaded', () => callback(query), false);
    }

    window.helpers = { ready, query };
})(window);
