using OrchardCore.DisplayManagement.Manifest;
using OrchardCore.Modules.Manifest;
using static Lombiq.HelpfulExtensions.FeatureIds;

[assembly: Theme(
    Name = "Lombiq Base Theme",
    Author = "Lombiq Technologies",
    Version = "0.0.1",
    Website = "https://github.com/Lombiq/Orchard-Base-Theme",
    Description = "The base frontend theme for shared code that is not specific to a specific project's theme." +
      "Warning: themes using this as the base remove the stock Bootstrap resource. If you switch to a different " +
      "theme, please reload the tenant from Configuration → Tenants in the admin menu.",
    Dependencies =
    [
        ContentTypes,
        Widgets,
    ]
)]
