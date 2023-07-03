using Lombiq.BaseTheme.Models;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using OrchardCore.Entities;
using OrchardCore.Media;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using System;

namespace Lombiq.BaseTheme.Services;

public class IconResourceFilter : IResourceFilterProvider
{
    private readonly IMediaFileStore _mediaFileStore;
    private readonly ISiteService _siteService;

    public IconResourceFilter(IMediaFileStore mediaFileStore, ISiteService siteService)
    {
        _mediaFileStore = mediaFileStore;
        _siteService = siteService;
    }

    public void AddResourceFilter(ResourceFilterBuilder builder) =>
        builder
            .When(async _ =>
            {
                var siteSettings = await _siteService.GetSiteSettingsAsync();
                return !string.IsNullOrEmpty(siteSettings.As<BaseThemeSettings>()?.Icon);
            })
            .ExecuteTask(async resourceManager =>
            {
                var settings = (await _siteService.GetSiteSettingsAsync()).As<BaseThemeSettings>();
                resourceManager.RegisterLink(new LinkEntry
                {
                    Href = $"{_mediaFileStore.MapPathToPublicUrl(settings.Icon)}?at={settings.TimeStamp.ToTechnicalString()}",
                    Rel = "shortcut icon",
                    Type = "image/x-icon",
                });
            });
}
