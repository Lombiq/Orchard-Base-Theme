using Lombiq.BaseTheme.Native.Samples.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using NativeResourceNames = Lombiq.BaseTheme.Native.Constants.ResourceNames;

namespace Lombiq.BaseTheme.Native.Samples;

// This class sets up the resources. We use a custom base class from Lombiq.HelpfulLibraries.OrchardCore to reduce
// boilerplate when using a standard directory structure and no generated minified files. You can learn more about
// registering resources using stock Orchard Core tools in the Training Demo's "Resource management" section.
public class ResourceManagementOptionsConfiguration : ResourceManagementOptionsConfiguratorBase
{
    protected override string Area => FeatureIds.Area;

    protected override void Configure(ResourceManagementContext context)
    {
        // Here we define these styles (under the hood: using the context.Manifest.DefineStyle() method) in the
        // ~/Lombiq.BaseTheme.Native.Samples/css/site.css and ~/Lombiq.BaseTheme.Native.Samples/css/pages/blog-posts.css
        // paths. We use dependencies to ensure that BlogPost is loaded after Site, and Site is loaded after the CSS
        // base theme's main stylesheet (which has its own dependencies). These resources are loaded in using the
        // resource filter middleware from Lombiq.HelpfulLibraries.OrchardCore, in the Startup class.
        context.DefineStyle(ResourceNames.Site, "site.css", NativeResourceNames.General);
        context.DefineStyle(ResourceNames.BlogPost, "pages/blog-post.css", ResourceNames.Site);
    }
}

// NEXT STATION: Startup.cs
