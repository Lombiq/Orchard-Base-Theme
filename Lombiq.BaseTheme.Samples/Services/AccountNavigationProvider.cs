using Lombiq.HelpfulExtensions.Extensions.Widgets.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace Lombiq.DataTables.Samples.Navigation;

// Normally you wouldn't put this service into the theme, but in your other modules where the functionality is (and if
// you are using the Lombiq Open Source Orchard Core Extensions repository then you can find other examples, e.g.
// DataTablesNavigationProvider), but we wanted to make the sample self-contained.

// This service adds a Login/Account menu to the website's main menu. The base class implements INavigationProvider, and
// wraps it so you don't have to do the menu name checking, because the name is always "main" which is the menu that the
// Lombiq Base Theme handles out of the box.
public class AccountNavigationProvider : MainMenuNavigationProviderBase
{
    public AccountNavigationProvider(
        IHttpContextAccessor hca,
        IStringLocalizer<AccountNavigationProvider> stringLocalizer)
        : base(hca, stringLocalizer)
    {
    }

    // The menu itself is built the same way as the Admin menus that Orchard already handles. Except due to limitations
    // of Bootstrap's navigation features you can't nest menus, you can only have two layers.
    // To learn more about navigation, see the Lombiq Training Demo for Orchard Core, specifically the "Admin menus"
    // training section: https://github.com/Lombiq/Orchard-Training-Demo-Module/blob/dev/Controllers/AdminController.cs
    protected override void Build(NavigationBuilder builder)
    {
        if (_hca.HttpContext?.User.Identity?.IsAuthenticated != true)
        {
            builder.Add(T["Log In"], builder => builder
                .LocalNav()
                .Action("Login", "Account", new { area = "OrchardCore.Users" }));
            return;
        }

        builder.Add(T["Account"], builder => builder
            .Add(T["Admin"], itemBuilder => itemBuilder.Action("Index", "Admin", new { area = "OrchardCore.Admin" }))
            .Add(T["Log Out"], itemBuilder => itemBuilder
                .LocalNav()
                .Action(
                    "LogOff",
                    "Account",
                    new RouteValueDictionary
                    {
                        ["area"] = "OrchardCore.Users",
                        // This is a special value recognized by the MenuWidget to indicate the link must be a button
                        // that sends a POST request instead of a link.
                        [RouteValueNames.MenuWidgetPost] = true,
                    })));
    }
}

// END OF TRAINING SECTION: Front-end navigation via the "main" menu

// NEXT STATION: Migrations/RecipeMigrations.cs
