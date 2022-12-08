using Lombiq.BaseTheme.Constants;
using OrchardCore.ResourceManagement;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Options;

public static class ResourceManagementOptionsExtensions
{
    private static readonly object _lock = new();

    private static readonly string[] _resourceTypes = { "stylesheet", "script" };

    public static void RemoveBootstrap5(this IOptions<ResourceManagementOptions> resourceManagementOptions)
    {
        var resourcesToClear = resourceManagementOptions
            .Value
            .ResourceManifests
            .Where(manifest => !manifest.GetResources("$" + nameof(FeatureIds.Area)).ContainsKey(FeatureIds.Area))
            .SelectMany(manifest => _resourceTypes
                .SelectWhere(manifest.GetResources)
                .SelectWhere(resources => resources.GetMaybe("bootstrap"), resource => resource?.Any() == true))
            .ToList();

        if (!resourcesToClear.Any()) return;

        lock (_lock)
        {
            foreach (var resource in resourcesToClear)
            {
                resource.Clear();
            }
        }
    }
}
