using Lombiq.BaseTheme.Native.Samples.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme.Native.Samples;

// This class sets up the resources. We use a custom base class from Lombiq.HelpfulLibraries.OrchardCore to reduce
// boilerplate when using a standard directory structure and no generated minified files. You can learn more about
// registering resources using stock Orchard Core tools in the Training Demo's "Resource management" section.
public class ResourceManagementOptionsConfiguration : ResourceManagementOptionsConfiguratorBase
{
    protected override string Area => FeatureIds.Area;

    protected override void Configure(ResourceManagementContext context)
    {
        // Here we define these styles (under the hood: using the context.Manifest.DefineStyle() method) e.g. in the
        // ~/Lombiq.BaseTheme.Native.Samples/css/general/general.css and
        // ~/Lombiq.BaseTheme.Native.Samples/css/pages/blog-posts.css paths. We use dependencies to ensure that BlogPost
        // is loaded after General, and General is loaded after its dependencies, and all are loaded after the Native
        // Base Theme's main stylesheet (which has its own dependencies). The DefineBaseThemeStyle method does the same
        // thing as DefineStyle, but implicitly adds the Lombiq.BaseTheme.Native package's general.css as dependency so
        // you should use it on your lowest level stylesheets that don't have other dependencies.
        context.DefineBaseThemeStyle(ResourceNames.NativeVariables, "abstract/native-variables.css");
        context.DefineStyle(ResourceNames.Helpers, "abstract/helpers.css", ResourceNames.NativeVariables);
        context.DefineStyle(ResourceNames.General, "general/general.css", ResourceNames.NativeVariables, ResourceNames.Helpers);
        context.DefineBaseThemeStyle(ResourceNames.Navigation, "general/navigation.css");
        context.DefineStyle(ResourceNames.BlogPost, "pages/blog-post.css", ResourceNames.General);

        context.DefineBaseThemeStyle(ResourceNames.Site, "site.css");
    }
}

// These resources have to be loaded in. One convenient was is using the resource filter middleware from
// Lombiq.HelpfulLibraries.OrchardCore, in the Startup class.

// NEXT STATION: Startup.cs
