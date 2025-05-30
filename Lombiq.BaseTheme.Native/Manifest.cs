using OrchardCore.DisplayManagement.Manifest;
using static Lombiq.BaseTheme.Core.Constants.FeatureIds;

[assembly: Theme(
    Name = "Lombiq Base Theme - Native CSS",
    Author = "Lombiq Technologies",
    Version = "0.0.1",
    Website = "https://github.com/Lombiq/Orchard-Base-Theme",
    Description = "The base frontend theme for shared code that is not specific to any particular project's theme.",
    Dependencies = [Core],
    BaseTheme = Core
)]
