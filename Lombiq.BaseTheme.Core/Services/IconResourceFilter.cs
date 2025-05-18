using Lombiq.BaseTheme.Core.Attributes;
using Lombiq.BaseTheme.Core.Models;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using OrchardCore;
using OrchardCore.Media;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using OrchardCore.Themes.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lombiq.BaseTheme.Core.Services;

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

                    var themeLinks = theme.Links;

                    if (themeLinks?.Any() == true)
                    {
                        themeLinks.ForEach(linkEntry => linkEntry.Href = _orchardHelper.ResourceUrl(linkEntry.Href));
                        themeLinks.ForEach(resourceManager.RegisterLink);
                    }
                }

                // If the site setting icon is set, that should take priority.
                if (await _siteService.GetSettingsAsync<BaseThemeSettings>() is { Icon.Length: > 0 } settings)
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
