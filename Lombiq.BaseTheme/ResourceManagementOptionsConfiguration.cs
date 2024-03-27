using Lombiq.BaseTheme.Constants;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;
using System;

namespace Lombiq.BaseTheme;

public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
{
    private const string WwwRoot = "~/" + FeatureIds.Area + "/";
    private const string Css = WwwRoot + "css/";
    private const string Js = WwwRoot + "js/";
    private const string Vendors = WwwRoot + "vendors/";

    // Make sure to keep this exactly the same version as the "bootstrap" dependency in the package.json file.
    public static readonly Version NpmBootstrapVersion = new(5, 3, 2);

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
            .SetUrl(Vendors + "bootstrap/js/bootstrap.bundle.min.js", Vendors + "bootstrap/js/bootstrap.bundle.js")
            .SetVersion(NpmBootstrapVersion.ToString());
    }

    public void Configure(ResourceManagementOptions options) => options.ResourceManifests.Add(_manifest);
}
