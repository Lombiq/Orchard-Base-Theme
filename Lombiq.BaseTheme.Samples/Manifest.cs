using OrchardCore.DisplayManagement.Manifest;
using static Lombiq.BaseTheme.Constants.FeatureIds;

// Make sure to set the BaseTheme value in your custom theme to "Lombiq.BaseTheme" and reference Lombiq.BaseTheme in
// your project file.
[assembly: Theme(
    Name = "Lombiq Base Theme - Samples",
    Author = "Lombiq Technologies",
    Version = "1.0",
    Website = "https://github.com/Lombiq/Orchard-Base-Theme",
    Description = "A sample theme that builds on Lombiq Base Theme.",
    BaseTheme = BaseTheme
)]
