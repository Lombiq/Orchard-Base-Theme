using Lombiq.BaseTheme.Core.Constants;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme.Core;

public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
{
    private const string WwwRoot = "~/" + FeatureIds.Area + "/";
    private const string Css = WwwRoot + "css/";

    private static readonly ResourceManifest _manifest = new();

    static ResourceManagementOptionsConfiguration()
    {
        _manifest
            .DefineStyle(ResourceNames.Helpers)
            .SetUrl(Css + "helpers.css");
        _manifest
            .DefineStyle(ResourceNames.NativeVariables)
            .SetUrl(Css + "native-variables.css");
    }

    public void Configure(ResourceManagementOptions options) => options.ResourceManifests.Add(_manifest);
}
