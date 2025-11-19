using Lombiq.BaseTheme.Native.Constants;
using OrchardCore.ResourceManagement;

namespace Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;

public static class ResourceManagementContextExtensions
{
    /// <summary>
    /// Define a style resource inside the <c>~/{Area}/css/{filename}</c> location that depends on the <see
    /// cref="ResourceNames.General"/> resource.
    /// </summary>
    public static ResourceDefinition DefineBaseThemeStyle(
        this ResourceManagementOptionsConfiguratorBase.ResourceManagementContext context,
        string resourceName,
        string fileName,
        params string[] dependencies) =>
        context.DefineStyle(resourceName, fileName, [ResourceNames.General, .. dependencies]);
}
