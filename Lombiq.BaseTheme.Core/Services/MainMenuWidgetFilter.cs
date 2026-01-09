using Lombiq.BaseTheme.Core.Constants;
using Lombiq.BaseTheme.Core.Models;
using Lombiq.HelpfulExtensions.Extensions.Widgets;
using Lombiq.HelpfulExtensions.Extensions.Widgets.ViewModels;
using Lombiq.HelpfulLibraries.OrchardCore.Mvc;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Extensions;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Core.Services;

public sealed class MainMenuWidgetFilter : WidgetFilterBase<MenuWidgetViewModel>
{
    private readonly IHttpContextAccessor _hca;
    private readonly INavigationManager _navigationManager;
    private readonly ICssClassHolder _cssClassHolder;
    private readonly ISiteService _siteService;

    protected override string ZoneName => ZoneNames.Navigation;
    protected override string ViewName => WidgetTypes.MenuWidget;
    protected override bool FrontEndOnly => true;

    public MainMenuWidgetFilter(
        IAuthorizationService authorizationService,
        IHttpContextAccessor hca,
        ILayoutAccessor layoutAccessor,
        IShapeFactory shapeFactory,
        INavigationManager navigationManager,
        ICssClassHolder cssClassHolder,
        ISiteService siteService)
        : base(requiredPermission: null, authorizationService, layoutAccessor, shapeFactory)
    {
        _hca = hca;
        _navigationManager = navigationManager;
        _cssClassHolder = cssClassHolder;
        _siteService = siteService;
    }

    protected override async Task<MenuWidgetViewModel> GetViewModelAsync()
    {
        if (_hca.HttpContext is not { } httpContext ||
            await _siteService.GetSettingsAsync<BaseThemeSettings>() is { HideMenu: true })
        {
            return null;
        }

        // Add the <nav> classes to the zone holder <nav>.
        _cssClassHolder.AddClassToZone(ZoneNames.Navigation, "navbar-expand-md");
        _cssClassHolder.AddClassToZone(ZoneNames.Navigation, "navbar");

        return new(
            noWrapper: true, // The navigation zone is already the wrapper.
            menuItems: await _navigationManager.BuildMenuAsync(
                MainMenuNavigationProviderBase.MainNavigationName,
                await httpContext.GetActionContextAsync()));
    }
}
