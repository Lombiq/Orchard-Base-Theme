using OrchardCore.DisplayManagement.Manifest;
using static Lombiq.HelpfulExtensions.FeatureIds;

[assembly: Theme(
    Name = "Lombiq Base Theme for Orchard Core (Core Features)",
    Author = "Lombiq Technologies",
    Version = "0.0.1",
    Website = "https://github.com/Lombiq/Orchard-Base-Theme",
    Description = "All the shared content between the Sass and native CSS versions of the base theme.",
    Dependencies =
    [
        ContentTypes,
        Widgets,
    ]
)]
