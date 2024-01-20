using Lombiq.BaseTheme.Constants;
using Lombiq.BaseTheme.Models;
using Lombiq.HelpfulExtensions.Extensions.Widgets;
using Lombiq.HelpfulExtensions.Extensions.Widgets.ViewModels;
using Lombiq.HelpfulLibraries.OrchardCore.Mvc;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Services;

public class MainMenuWidgetFilter(
    IAuthorizationService authorizationService,
    ILayoutAccessor layoutAccessor,
    IShapeFactory shapeFactory,
    INavigationManager navigationManager,
    IActionContextAccessor actionContextAccessor,
    ICssClassHolder cssClassHolder,
    ISiteService siteService) : WidgetFilterBase<MenuWidgetViewModel>(requiredPermission: null, authorizationService, layoutAccessor, shapeFactory)
{
    protected override string ZoneName => ZoneNames.Navigation;
    protected override string ViewName => WidgetTypes.MenuWidget;
    protected override bool FrontEndOnly => true;

    protected override async Task<MenuWidgetViewModel> GetViewModelAsync()
    {
        var siteSettings = await siteService.GetSiteSettingsAsync();
        if (siteSettings.As<BaseThemeSettings>()?.HideMenu == true) return null;

        // Add the <nav> classes to the zone holder <nav>.
        cssClassHolder.AddClassToZone(ZoneNames.Navigation, "navbar-expand-md");
        cssClassHolder.AddClassToZone(ZoneNames.Navigation, "navbar");

        return new()
        {
            MenuItems = await navigationManager.BuildMenuAsync(
                MainMenuNavigationProviderBase.MainNavigationName,
                actionContextAccessor.ActionContext),
            NoWrapper = true, // The navigation zone is already the wrapper.
        };
    }
}
