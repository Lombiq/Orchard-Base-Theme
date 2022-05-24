﻿using Lombiq.BaseTheme.Samples.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;

namespace Lombiq.DataTables.Samples.Navigation;

// This service implements conditional configuration to direct the ResourceFilterMiddleware when and what styles need to
// be added to the resources. The attribute indicates that the middleware should only invoke this provider if the
// current admin or site theme is equal to the one in the attribute constructor.
[ResourceFilterThemeRequirement(FeatureIds.BaseThemeSamples)]
public class ResourceFilters : IResourceFilterProvider
{
    public void AddResourceFilter(ResourceFilterBuilder builder)
    {
        // We always want to add the site style. If you only have "Always" styles and you are going to override
        // Views/Widget-LayoutInjection.cshtml anyway, then it's easier to inject your style there and skip using a
        // ResourceFilterMiddleware altogether.
        builder.Always().RegisterStylesheet(ResourceNames.Site);
        builder.WhenContentType("BlogPost").RegisterStylesheet(ResourceNames.BlogPost);
    }
}

// END OF TRAINING SECTION: Configuration

// NEXT STATION: Views/Widget-LayoutInjection.cshtml
