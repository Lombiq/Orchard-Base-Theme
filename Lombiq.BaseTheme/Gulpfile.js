const recommendedSetup = require('../../Utilities/Lombiq.Gulp.Extensions/recommended-setup');

const vendorBasePath = './node_modules/';
const vendorAssets = [
    {
        name: 'bootstrap',
        path: vendorBasePath + 'bootstrap/dist/**/*.*',
    },
];

recommendedSetup.setupRecommendedScssAndJsTasksAndVendorsCopyAssets(vendorAssets)
