using Lombiq.BaseTheme.Constants;
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

namespace Lombiq.BaseTheme.Services
{
    public class PortalMenuWidgetFilter : WidgetFilterBase<PortalMenuWidgetViewModel>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IEnumerable<INavigationProvider> _navigationProviders;
        private readonly IHttpContextAccessor _hca;

        protected override string ZoneName => ZoneNames.Navigation;
        protected override string ViewName => WidgetTypes.PortalMenuWidget;
        protected override bool AdminOnly => false;

        public PortalMenuWidgetFilter(
            IAuthorizationService authorizationService,
            ILayoutAccessor layoutAccessor,
            IShapeFactory shapeFactory,
            IEnumerable<INavigationProvider> navigationProviders,
            IHttpContextAccessor hca)
            : base(requiredPermission: null, authorizationService, layoutAccessor, shapeFactory)
        {
            _authorizationService = authorizationService;
            _navigationProviders = navigationProviders;
            _hca = hca;
        }

        protected override async Task<PortalMenuWidgetViewModel> GetViewModelAsync() =>
            new()
            {
                MenuItems = await PortalMenuWidgetDisplayDriver.GetMenuItemsAsync(
                    _authorizationService,
                    _navigationProviders,
                    _hca),
            };
    }
}
