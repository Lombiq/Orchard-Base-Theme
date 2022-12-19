using Lombiq.BaseTheme.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement.Manifest;
using OrchardCore.Environment.Extensions;
using OrchardCore.ResourceManagement;
using OrchardCore.Themes.Services;
using OrchardCore.Workflows.Helpers;
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
    private static readonly string[] _resourceTypes = { "stylesheet", "script" };

    private readonly RequestDelegate _next;

    public RemoveBootstrapMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(
        HttpContext context,
        IOptions<ResourceManagementOptions> resourceManagementOptions,
        ISiteThemeService siteThemeService)
    {
        if (IsCurrentTheme(await siteThemeService.GetSiteThemeAsync()))
        {
            var resourcesToClear = resourceManagementOptions
                .Value
                .ResourceManifests
                .Where(manifest => !manifest.GetResources("$" + nameof(FeatureIds.Area)).ContainsKey(FeatureIds.Area))
                .SelectMany(manifest => _resourceTypes
                    .SelectWhere(manifest.GetResources)
                    .SelectWhere(
                        resources => resources.GetMaybe("bootstrap"),
                        resource => resource?.Any(IsVersion5) == true))
                .ToList();

            // This only happens once per tenant process instance, so locking is rare.
            if (resourcesToClear.Any()) ClearResources(resourcesToClear);
        }

        await _next(context);
    }

    private static bool IsCurrentTheme(IExtensionInfo currentSiteTheme) =>
        currentSiteTheme?.Id == FeatureIds.BaseTheme ||
        (currentSiteTheme?.Manifest.ModuleInfo as ThemeAttribute)?.BaseTheme == FeatureIds.BaseTheme;

    private static bool IsVersion5(ResourceDefinition definition) =>
        Version.TryParse(definition.Version, out var version) &&
        version.Major == 5;

    private static void ClearResources(IEnumerable<IList<ResourceDefinition>> resourcesToClear)
    {
        lock (_lock)
        {
            foreach (var resource in resourcesToClear)
            {
                var toRemove = resource.Where(IsVersion5).ToList();
                resource.RemoveRange(toRemove);
            }
        }
    }
}
