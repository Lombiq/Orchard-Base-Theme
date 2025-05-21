using Lombiq.BaseTheme.Core.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme.Core;

public class ResourceManagementOptionsConfiguration : ResourceManagementOptionsConfiguratorBase
{
    protected override string Area => FeatureIds.Area;

    protected override void Configure(ResourceManagementContext context)
    {
        context.DefineStyle(ResourceNames.General, "general.css");
        context.DefineStyle(ResourceNames.Helpers, "helpers.css");
        context.DefineStyle(ResourceNames.NativeVariables, "native-variables.css");
    }
}
