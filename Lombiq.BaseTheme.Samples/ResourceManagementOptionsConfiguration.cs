using Lombiq.BaseTheme.Samples.Constants;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme.Samples;

// This class sets up the generated .css files as resources. Make sure to include every output (so all files whose .scss
// doesn't start with an underscore) so they can be referenced
public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
{
    private const string WwwRoot = "~/" + FeatureIds.Area + "/";
    private const string Css = WwwRoot + "css/";

    private static readonly ResourceManifest _manifest = new();

    static ResourceManagementOptionsConfiguration()
    {
        // Learn more about registering resources in the Training Demo's "Resource management" section.
        _manifest
            .DefineStyle(ResourceNames.Site)
            .SetUrl(Css + "site.min.css", Css + "site.css");

        _manifest
            .DefineStyle(ResourceNames.BlogPost)
            .SetUrl(Css + "pages/blog-post.min.css", Css + "pages/blog-post.css");
    }

    public void Configure(ResourceManagementOptions options) => options.ResourceManifests.Add(_manifest);
}

// NEXT STATION: Startup.cs
