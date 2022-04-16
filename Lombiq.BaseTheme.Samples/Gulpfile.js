// We are using the prepared functions from the Lombiq Gulp Extensions repo to set up Gulp. See also the package.json
// file or the Gulp Extensions readme.
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

// In a theme you typically need SCSS compiling and asset copying. You don't need scripts here, but it's simpler and
// more future-proof to create an empty ./Assets/Scripts directory so you can use this combined function instead of
// separately adding the style and asset tasks.
recommendedSetup.setupRecommendedScssAndJsTasksAndCopyAssets(staticAssets);

// NEXT STATION: ResourceManagementOptionsConfiguration.cs
