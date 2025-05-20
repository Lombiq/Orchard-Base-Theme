using Lombiq.BaseTheme.Constants;
using Lombiq.HelpfulLibraries.Attributes;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;
using CoreResourceNames = Lombiq.BaseTheme.Core.Constants.ResourceNames;

namespace Lombiq.BaseTheme;

[ConstantFromJson("NpmBootstrapVersion", "package.json", "bootstrap")]
public partial class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
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
            .SetUrl(Css + "site.min.css", Css + "site.css")
            .SetDependencies(CoreResourceNames.Helpers, CoreResourceNames.NativeVariables);

        _manifest
            .DefineScript(ResourceNames.Helpers)
            .SetUrl(Js + "helpers.js");

        _manifest
            .DefineScript("bootstrap")
            .SetUrl(Vendors + "bootstrap/js/bootstrap.bundle.min.js", Vendors + "bootstrap/js/bootstrap.bundle.js")
            .SetVersion(NpmBootstrapVersion);
    }

    public void Configure(ResourceManagementOptions options) => options.ResourceManifests.Add(_manifest);
}
