using OrchardCore.DisplayManagement.Manifest;
using static Lombiq.BaseTheme.Constants.FeatureIds;

// Theme manifests in Orchard Core are similar to module manifests (see "Module manifest" section in the Training Demo),
// except you have to use the Theme attribute and set the BaseTheme value to the constant at
// Lombiq.BaseTheme.Constants.FeatureIds.BaseTheme from the Lombiq.BaseTheme project.
[assembly: Theme(
    Name = "Lombiq Base Theme - Samples",
    Author = "Lombiq Technologies",
    Version = "1.0.0-alpha",
    Website = "https://github.com/Lombiq/Orchard-Base-Theme",
    Description = "A sample theme that builds on Lombiq Base Theme.",
    // A base theme is another theme project. Orchard Core Display Management first searches your theme and then the
    // base theme for template alternates. Besides that, it's similar to a dependency in modules, so any services
    // registered in the base theme are also accessible.
    BaseTheme = BaseTheme
)]

// Steps you need to do outside of this project:
// - Reference the project in your web app.
// - Enable the "Lombiq.BaseTheme.Samples" feature in your setup recipe.
// - Set the site theme to "Lombiq.BaseTheme.Samples" in your setup recipe.
// - Run the "Lombiq.BaseTheme.LayersAndZones" recipe from your setup recipe.
// You can see these in the "Lombiq.OSOCE.Web.csproj" and the "Lombiq.OSOCE.Tests.recipe.json" files in the
// https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions/ repository.

// NEXT STATION: Gulpfile.js
