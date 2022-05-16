using Lombiq.BaseTheme.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;

namespace Lombiq.DataTables.Navigation;

[ResourceFilterThemeRequirement(FeatureIds.BaseTheme)]
public class ResourceFilters : IResourceFilterProvider
{
    public void AddResourceFilter(ResourceFilterBuilder builder) =>
        builder.Always().RegisterStylesheet(ResourceNames.Site);
}
