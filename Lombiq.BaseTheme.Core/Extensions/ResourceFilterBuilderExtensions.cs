using Lombiq.BaseTheme.Core.Constants;

namespace Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;

public static class ResourceFilterBuilderExtensions
{
    /// <summary>
    /// Registers common resources between different Lombiq.BaseTheme implementations.
    /// </summary>
    public static ResourceFilter RegisterBaseThemeCoreResources(this ResourceFilter builder) =>
        builder
            .RegisterStylesheet(ResourceNames.Bootstrap)
            .RegisterHeadScript(ResourceNames.Bootstrap)
            .RegisterStylesheet(ResourceNames.Helpers)
            .RegisterStylesheet(ResourceNames.NativeVariables);
}
