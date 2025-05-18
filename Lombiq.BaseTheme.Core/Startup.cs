using Lombiq.BaseTheme.Core.Migrations;
using Lombiq.BaseTheme.Core.Navigation;
using Lombiq.BaseTheme.Core.Permissions;
using Lombiq.BaseTheme.Core.Services;
using Lombiq.HelpfulLibraries.AspNetCore.Extensions;
using Lombiq.HelpfulLibraries.OrchardCore.Shapes;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;

namespace Lombiq.BaseTheme.Core;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICssClassHolder, CssClassHolder>();
        services.AddAsyncResultFilter<MainMenuWidgetFilter>();

        services.AddDataMigration<LayoutInjectionMigrations>();
        services.AddDataMigration<RecipeMigrations>();

        services.AddResourceFilter<IconResourceFilter>();

        PerTenantShapeTableManager.ReplaceDefaultShapeTableManager(services);
        services.AddNavigationProvider<MainMenuNavigationProvider>();

        services.AddPermissionProvider<BaseThemeSettingsPermissions>();
        services.AddNavigationProvider<BaseThemeSettingsAdminMenu>();
    }
}
