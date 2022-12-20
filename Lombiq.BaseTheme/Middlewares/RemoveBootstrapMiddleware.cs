using Lombiq.BaseTheme.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement.Manifest;
using OrchardCore.Environment.Extensions;
using OrchardCore.ResourceManagement;
using OrchardCore.Themes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Middlewares;

/// <summary>
/// Removes the built-in Bootstrap resource if the currently selected theme uses Lombiq.BaseTheme as its base theme.
/// Themes derived from Lombiq.BaseTheme use Bootstrap from NPM as it's compiled into their site stylesheet. So this
/// duplicate resource is not needed and can cause problems if not removed. This situation can arise when a module
/// (such as Lombiq.DataTables) depends on Bootstrap and doesn't explicitly depend on Lombiq.BaseTheme so the built-in
/// resource would be injected if this middleware didn't remove it.
/// </summary>
public class RemoveBootstrapMiddleware
{
    private static readonly object _lock = new();

    private readonly RequestDelegate _next;

    public RemoveBootstrapMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(
        HttpContext context,
        IOptions<ResourceManagementOptions> resourceManagementOptions,
        ISiteThemeService siteThemeService)
    {
        if (IsCurrentTheme(await siteThemeService.GetSiteThemeAsync()))
        {
            var resourcesToClear = new List<(IList<ResourceDefinition> Resources, ResourceDefinition ResourceToDelete)>();
            resourcesToClear.AddRange(GetResourcesToClear(resourceManagementOptions, "stylesheet", keepNewest: false));
            resourcesToClear.AddRange(GetResourcesToClear(resourceManagementOptions, "script", keepNewest: true));

            // This only happens once per tenant process instance, so locking is rare.
            if (resourcesToClear.Any()) ClearResources(resourcesToClear);
        }

        await _next(context);
    }

    private static bool IsCurrentTheme(IExtensionInfo currentSiteTheme) =>
        currentSiteTheme?.Id == FeatureIds.BaseTheme ||
        (currentSiteTheme?.Manifest.ModuleInfo as ThemeAttribute)?.BaseTheme == FeatureIds.BaseTheme;

    private static IEnumerable<(IList<ResourceDefinition> Resources, ResourceDefinition ResourceToDelete)> GetResourcesToClear(
        IOptions<ResourceManagementOptions> resourceManagementOptions,
        string resourceType,
        bool keepNewest)
    {
        var resources = resourceManagementOptions
            .Value
            .ResourceManifests
            .SelectWhere(manifest => manifest.GetResources(resourceType)?.GetMaybe("bootstrap"))
            .SelectMany(resources => resources
                .Where(definition => Version.TryParse(definition.Version, out var version) && version.Major == 5)
                .Select(resource => (Resources: resources, ResourceToDelete: resource)))
            .ToList();

        if (keepNewest)
        {
            // When there is only one item in the collection and we want to keep the newest entry that means not
            // removing anything. Also if there is nothing in the list anyway we can shortcut return here to skip the
            // steps below.
            if (resources.Count < 2)
            {
                return Enumerable.Empty<(IList<ResourceDefinition> Resources, ResourceDefinition ResourceToDelete)>();
            }

            var newest = resources.MaxBy(resource => Version.Parse(resource.ResourceToDelete.Version));
            resources.Remove(newest);
        }

        return resources;
    }

    private static void ClearResources(IEnumerable<(IList<ResourceDefinition> Resources, ResourceDefinition ResourceToDelete)> resourcesToClear)
    {
        lock (_lock)
        {
            foreach (var (resources, toDelete) in resourcesToClear)
            {
                resources.Remove(toDelete);
            }
        }
    }
}
