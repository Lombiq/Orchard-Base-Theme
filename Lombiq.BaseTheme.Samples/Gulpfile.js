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

// In a theme you typically need SCSS compiling and asset copying. Typically you don't need scripts, but it's simpler to
// and create an empty Assets/Scripts directory so you can use this combined function instead of separately adding the
// style and asset tasks.
recommendedSetup.setupRecommendedScssAndJsTasksAndCopyAssets(staticAssets);
