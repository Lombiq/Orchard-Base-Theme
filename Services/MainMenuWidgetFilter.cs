using Lombiq.HelpfulExtensions.Extensions.Widgets;
using Lombiq.HelpfulExtensions.Extensions.Widgets.ViewModels;
using Lombiq.HelpfulLibraries.Libraries.Mvc;
using Lombiq.HelpfulLibraries.Libraries.Navigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.Navigation;
using System.Threading.Tasks;
using static Lombiq.BaseTheme.Constants.ZoneNames;

namespace Lombiq.BaseTheme.Services
{
    public class MainMenuWidgetFilter : WidgetFilterBase<MenuWidgetViewModel>
    {
        private readonly INavigationManager _navigationManager;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ICssClassHolder _cssClassHolder;

        protected override string ZoneName => Navigation;
        protected override string ViewName => WidgetTypes.MenuWidget;
        protected override bool FrontEndOnly => true;

        public MainMenuWidgetFilter(
            IAuthorizationService authorizationService,
            ILayoutAccessor layoutAccessor,
            IShapeFactory shapeFactory,
            INavigationManager navigationManager,
            IActionContextAccessor actionContextAccessor,
            ICssClassHolder cssClassHolder)
            : base(requiredPermission: null, authorizationService, layoutAccessor, shapeFactory)
        {
            _navigationManager = navigationManager;
            _actionContextAccessor = actionContextAccessor;
            _cssClassHolder = cssClassHolder;
        }

        protected override async Task<MenuWidgetViewModel> GetViewModelAsync()
        {
            // Add the <nav> classes to the zone holder <nav>.
            _cssClassHolder.AddClassToZone(Navigation, "navbar-expand-md");
            _cssClassHolder.AddClassToZone(Navigation, "navbar");

            return new()
            {
                MenuItems = await _navigationManager.BuildMenuAsync(
                    MainMenuNavigationProviderBase.MainNavigationName,
                    _actionContextAccessor.ActionContext),
                NoWrapper = true, // The navigation zone is already the wrapper.
            };
        }
    }
}
