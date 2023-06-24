using Lombiq.BaseTheme.Drivers;
using Lombiq.BaseTheme.Permissions;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace Lombiq.BaseTheme.Navigation;

public class BaseThemeSettingsAdminMenu : AdminMenuNavigationProviderBase
{
    protected BaseThemeSettingsAdminMenu(IHttpContextAccessor hca, IStringLocalizer stringLocalizer)
        : base(hca, stringLocalizer)
    {
    }

    protected override void Build(NavigationBuilder builder) =>
        builder.Add(T["Configuration"], configuration => configuration
            .Add(T["Settings"], settings => settings
                .Add(T["Base Theme"], T["Base Theme"], baseTheme => baseTheme
                    .SiteSettings(BaseThemeSettingsDisplayDriver.EditorGroupId)
                    .Permission(BaseThemeSettingsPermissions.ManageBaseThemeSettings)
                    .LocalNav()
                )));
}
