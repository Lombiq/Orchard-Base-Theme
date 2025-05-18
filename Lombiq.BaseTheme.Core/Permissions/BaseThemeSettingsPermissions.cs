using Lombiq.HelpfulLibraries.OrchardCore.Users;
using OrchardCore.Security.Permissions;
using System.Collections.Generic;

namespace Lombiq.BaseTheme.Permissions;

public sealed class BaseThemeSettingsPermissions : AdminPermissionBase
{
    public static readonly Permission ManageBaseThemeSettings =
        new(nameof(ManageBaseThemeSettings), "Manage Lombiq.BaseTheme Settings.");

    protected override IEnumerable<Permission> AdminPermissions => [ManageBaseThemeSettings];
}
