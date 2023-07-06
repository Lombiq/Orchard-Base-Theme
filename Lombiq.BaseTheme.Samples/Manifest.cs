using Lombiq.BaseTheme.Attributes;

// Theme manifests in Orchard Core are similar to module manifests (see "Module manifest" section in the Training Demo),
// except you have to use the Theme attribute. DerivedTheme is a specific variant of Theme where the BaseTheme property
// is automatically set to the constant at Lombiq.BaseTheme.Constants.FeatureIds.BaseTheme from the Lombiq.BaseTheme
// project and it has some additional properties.
[assembly: DerivedTheme(
    Name = "Lombiq Base Theme - Samples",
    Author = "Lombiq Technologies",
    Version = "0.0.1",
    Website = "https://github.com/Lombiq/Orchard-Base-Theme",
    Description = "A sample theme that builds on Lombiq Base Theme.",
    // This is a new property in DerivedTheme. By setting it to a static resource you can define a default icon for this
    // theme. You can define other "link" resources too, using the Link property.
    Favicon = "~/Lombiq.BaseTheme.Samples/icons/favicon.ico"
)]

// Steps you need to do outside of this project:
// - Reference the project in your web app.
// - Enable the "Lombiq.BaseTheme.Samples" feature in your setup recipe.
// - Set the site theme to "Lombiq.BaseTheme.Samples" in your setup recipe.
// You can see these in the "Lombiq.OSOCE.Web.csproj" and the "Lombiq.OSOCE.Tests.recipe.json" files in the
// https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions/ repository.

// NEXT STATION: package.json
