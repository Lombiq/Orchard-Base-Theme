using Lombiq.BaseTheme.Constants;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private const string WwwRoot = "~/" + FeatureIds.Area + "/";
        private const string Css = WwwRoot + "css/";

        private static readonly ResourceManifest _manifest = new();

        static ResourceManagementOptionsConfiguration() =>
            _manifest
                .DefineStyle(ResourceNames.Site)
                .SetUrl(Css + "site.min.css", Css + "site.css");

        public void Configure(ResourceManagementOptions options) => options.ResourceManifests.Add(_manifest);
    }
}
