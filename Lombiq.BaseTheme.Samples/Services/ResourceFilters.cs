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

// Additional steps outside of this project:
// - Reference the project in your web app.
// - Enable the "Lombiq.BaseTheme.Samples" feature in your setup recipe.
// - Set the site theme to "Lombiq.BaseTheme.Samples" in your setup recipe.
// - Run the "Lombiq.BaseTheme.LayersAndZones" recipe from your setup recipe.
// You can see these in the "Lombiq.OSOCE.Web.csproj" and the "Lombiq.OSOCE.Tests.recipe.json" files in the
// https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions/ repository.

// END OF TRAINING SECTION: Configuration

// NEXT STATION: Views/Widget-LayoutInjection.cshtml
