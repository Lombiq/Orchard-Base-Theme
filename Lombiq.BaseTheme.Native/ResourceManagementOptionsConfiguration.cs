using Lombiq.BaseTheme.Native.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using CoreResourceNames = Lombiq.BaseTheme.Core.Constants.ResourceNames;

namespace Lombiq.BaseTheme.Native;

public class ResourceManagementOptionsConfiguration : ResourceManagementOptionsConfiguratorBase
{
    protected override string Area => FeatureIds.Area;

    protected override void Configure(ResourceManagementContext context)
    {
        context.DefineStyle(
            ResourceNames.General,
            "general.css",
            CoreResourceNames.General,
            CoreResourceNames.Helpers,
            CoreResourceNames.NativeVariables);

        context.DefineScript(ResourceNames.GridBreakpoints, "grid-breakpoints.js");
    }
}
