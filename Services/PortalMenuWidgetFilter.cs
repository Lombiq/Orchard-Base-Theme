using Lombiq.HelpfulExtensions.Extensions.Widgets;
using Lombiq.HelpfulExtensions.Extensions.Widgets.ViewModels;
using Lombiq.HelpfulLibraries.Libraries.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Lombiq.BaseTheme.Constants.ZoneNames;

namespace Lombiq.BaseTheme.Services
{
    public class PortalMenuWidgetFilter : WidgetFilterBase<PortalMenuWidgetViewModel>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IEnumerable<INavigationProvider> _navigationProviders;
        private readonly IHttpContextAccessor _hca;
        private readonly ICssClassHolder _cssClassHolder;

        protected override string ZoneName => Navigation;
        protected override string ViewName => WidgetTypes.PortalMenuWidget;
        protected override bool AdminOnly => false;

        public PortalMenuWidgetFilter(
            IAuthorizationService authorizationService,
            ILayoutAccessor layoutAccessor,
            IShapeFactory shapeFactory,
            IEnumerable<INavigationProvider> navigationProviders,
            IHttpContextAccessor hca,
            ICssClassHolder cssClassHolder)
            : base(requiredPermission: null, authorizationService, layoutAccessor, shapeFactory)
        {
            _authorizationService = authorizationService;
            _navigationProviders = navigationProviders;
            _hca = hca;
            _cssClassHolder = cssClassHolder;
        }

        protected override async Task<PortalMenuWidgetViewModel> GetViewModelAsync()
        {
            // Add the <nav> classes to the zone holder <nav>.
            _cssClassHolder.AddClassToZone(Navigation, "navbar-expand-md");
            _cssClassHolder.AddClassToZone(Navigation, "navbar");

            return new()
            {
                MenuItems = await PortalMenuWidgetDisplayDriver.GetMenuItemsAsync(
                    _authorizationService,
                    _navigationProviders,
                    _hca),
                NoWrapper = true, // The navigation zone is already the wrapper.
            };
        }
    }
}
