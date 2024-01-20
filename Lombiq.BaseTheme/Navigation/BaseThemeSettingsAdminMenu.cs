using Lombiq.BaseTheme.Controllers;
using Lombiq.BaseTheme.Permissions;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace Lombiq.BaseTheme.Navigation;

public class BaseThemeSettingsAdminMenu(
    IHttpContextAccessor hca,
    IStringLocalizer<BaseThemeSettingsAdminMenu> stringLocalizer) : AdminMenuNavigationProviderBase(hca, stringLocalizer)
{
    protected override void Build(NavigationBuilder builder) =>
        builder.Add(T["Configuration"], configuration => configuration
            .Add(T["Settings"], settings => settings
                .Add(T["Base Theme"], T["Base Theme"], baseTheme => baseTheme
                    .ActionTask<AdminController>(_hca.HttpContext, controller => controller.Index())
                    .Permission(BaseThemeSettingsPermissions.ManageBaseThemeSettings)
                    .LocalNav()
                )));
}
