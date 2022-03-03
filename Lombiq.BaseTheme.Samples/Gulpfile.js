const recommendedSetup = require('../../../Utilities/Lombiq.Gulp.Extensions/recommended-setup');

const assetsBasePath = './Assets/';
const staticAssets = [
    {
        name: 'icons',
        path: assetsBasePath + 'Icons/*',
    },
    {
        name: 'images',
        path: assetsBasePath + 'Images/*',
    },
];

recommendedSetup.setupRecommendedScssAndJsTasksAndCopyAssets(staticAssets);
