// We are using the prepared functions from the Lombiq Gulp Extensions repo to set up Gulp. See also the package.json
// file or the Gulp Extensions Readme.
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

const includePaths = [
    recommendedSetup.getNugetPath('Lombiq.BaseTheme', null, 'contentFiles', null, null, 'Assets', 'Styles')
];

// This method takes an object with three optional properties: assets, styles and scripts. Specifying them enables the
// corresponding Gulp task.
recommendedSetup.setupRecommended({
    // Assets must be an Array to enable.
    assets: staticAssets,

    // Scripts can be a "truthy" value, either true or an object with distBasePath and assetsBasePath properties. If
    // it's a "falsey" value like false, null or undefined (when you skip this property) then the task is not enabled.
    // Here we have no need for scripts.
    // scripts: true,

    // Styles can be specified the same way, however is has an additional "includePaths" property that expands the Sass
    // compiler's search path. Use this with the getNugetPath function like above if you added Lombiq.BaseTheme from a
    // NuGet package. You don't need it if you included it via Git submodule.
    // styles: true,
    styles: { includePaths },
});

// NEXT STATION: ResourceManagementOptionsConfiguration.cs
