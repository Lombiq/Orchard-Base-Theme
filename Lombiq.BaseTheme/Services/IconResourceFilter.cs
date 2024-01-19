using Lombiq.BaseTheme.Attributes;
using Lombiq.BaseTheme.Models;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using OrchardCore;
using OrchardCore.Media;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using OrchardCore.Themes.Services;
using System;
using System.Collections.Generic;

namespace Lombiq.BaseTheme.Services;

public class IconResourceFilter : IResourceFilterProvider
{
    private readonly IOrchardHelper _orchardHelper;
    private readonly IMediaFileStore _mediaFileStore;
    private readonly ISiteService _siteService;
    private readonly ISiteThemeService _siteThemeService;

    public IconResourceFilter(
        IOrchardHelper orchardHelper,
        IMediaFileStore mediaFileStore,
        ISiteService siteService,
        ISiteThemeService siteThemeService)
    {
        _orchardHelper = orchardHelper;
        _mediaFileStore = mediaFileStore;
        _siteService = siteService;
        _siteThemeService = siteThemeService;
    }

    public void AddResourceFilter(ResourceFilterBuilder builder) =>
        builder
            .Always()
            .ExecuteTask(async resourceManager =>
            {
                // Use static link resources as a fallback.
                var currentTheme = await _siteThemeService.GetSiteThemeAsync();
                if (currentTheme.Manifest.ModuleInfo is DerivedThemeAttribute theme)
                {
                    if (!string.IsNullOrEmpty(theme.Favicon))
                    {
                        var icon = theme.Favicon.StartsWithOrdinal("~/")
                            ? _orchardHelper.ResourceUrl(theme.Favicon)
                            : theme.Favicon;
                        AddIcon(resourceManager, icon);
                    }

                    theme.Links?.ForEach(resourceManager.RegisterLink);
                }

                // If the site setting icon is set, that should take priority.
                if ((await _siteService.GetSiteSettingsAsync()).As<BaseThemeSettings>() is { } settings &&
                    !string.IsNullOrEmpty(settings.Icon))
                {
                    var path = _mediaFileStore.MapPathToPublicUrl(settings.Icon);
                    AddIcon(resourceManager, $"{path}?at={settings.TimeStamp.ToTechnicalString()}");
                }
            });

    private static void AddIcon(IResourceManager resourceManager, string href) =>
        resourceManager.RegisterLink(new LinkEntry
        {
            Href = href,
            Rel = "shortcut icon",
            Type = "image/x-icon",
        });
}
