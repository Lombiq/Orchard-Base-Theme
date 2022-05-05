using Lombiq.BaseTheme.Samples.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;

namespace Lombiq.DataTables.Samples.Navigation;

// This service implements conditional configuration to direct the ResourceFilterMiddleware when and what styles need to
// be added to the resources.
public class ResourceFilters : IResourceFilterProvider
{
    public void AddResourceFilter(ResourceFilterBuilder builder)
    {
        // We always want to add the site style. If you only have "Always" styles then it's easier to inject them via
        // the Views/Widget-LayoutInjection.cshtml file and skip using a ResourceFilterMiddleware.
        builder.Always().RegisterStylesheet(ResourceNames.Site);
        builder.WhenContentType("BlogPost").RegisterStylesheet(ResourceNames.BlogPost);
    }
}

// END OF TRAINING SECTION: Configuration

// NEXT STATION: Views/Widget-LayoutInjection.cshtml
