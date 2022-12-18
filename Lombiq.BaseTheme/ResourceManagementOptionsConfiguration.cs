using Lombiq.BaseTheme.Constants;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme;

public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
{
    private const string WwwRoot = "~/" + FeatureIds.Area + "/";
    private const string Css = WwwRoot + "css/";
    private const string Js = WwwRoot + "js/";
    private const string Vendors = WwwRoot + "vendors/";

    private static readonly ResourceManifest _manifest = new();

    static ResourceManagementOptionsConfiguration()
    {
        _manifest.DefineResource("$" + nameof(FeatureIds.Area), FeatureIds.Area);

        _manifest
            .DefineStyle(ResourceNames.Site)
            .SetUrl(Css + "site.min.css", Css + "site.css");

        _manifest
            .DefineScript(ResourceNames.Helpers)
            .SetUrl(Js + "helpers.js");

        _manifest
            .DefineScript("bootstrap")
            .SetUrl(Vendors + "bootstrap/js/bootstrap.min.js", Vendors + "bootstrap/js/bootstrap.js")
            .SetVersion("5.1.3");
    }

    public void Configure(ResourceManagementOptions options) => options.ResourceManifests.Add(_manifest);
}
