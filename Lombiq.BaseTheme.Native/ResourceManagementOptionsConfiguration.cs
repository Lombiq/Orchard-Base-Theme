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
            ResourceNames.NativeVariables,
            "abstract/native-variables.css",
            CoreResourceNames.NativeVariables);

        context.DefineStyle(
            ResourceNames.Flow,
            "general/flow.css",
            ResourceNames.NativeVariables);

        context.DefineStyle(
            ResourceNames.General,
            "general/general.css",
            CoreResourceNames.General,
            CoreResourceNames.Helpers,
            CoreResourceNames.NativeVariables,
            ResourceNames.NativeVariables,
            ResourceNames.Flow);

        context.DefineScript(ResourceNames.GridBreakpoints, "grid-breakpoints.js");
    }
}
