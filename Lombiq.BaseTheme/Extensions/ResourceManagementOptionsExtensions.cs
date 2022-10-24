using Lombiq.BaseTheme.Constants;
using OrchardCore.ResourceManagement;

namespace Microsoft.Extensions.Options;

public static class ResourceManagementOptionsExtensions
{
    private static readonly object _lock = new();

    public static void RemoveBootstrap5(this IOptions<ResourceManagementOptions> resourceManagementOptions)
    {
        lock (_lock)
        {
            foreach (var manifest in resourceManagementOptions.Value.ResourceManifests)
            {
                if (manifest.GetResources("$" + nameof(FeatureIds.Area)).ContainsKey(FeatureIds.Area)) continue;

                foreach (var resourceType in new[] { "stylesheet", "script" })
                {
                    if (manifest.GetResources(resourceType).TryGetValue("bootstrap", out var resource))
                    {
                        resource.Clear();
                    }
                }
            }
        }
    }
}
