using Lombiq.BaseTheme.Middlewares;
using Lombiq.BaseTheme.Migrations;
using Lombiq.BaseTheme.Navigation;
using Lombiq.BaseTheme.Permissions;
using Lombiq.BaseTheme.Services;
using Lombiq.DataTables.Navigation;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Lombiq.HelpfulLibraries.OrchardCore.Shapes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Security.Permissions;
using System;

namespace Lombiq.BaseTheme;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICssClassHolder, CssClassHolder>();
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        services.Configure<MvcOptions>(options => options.Filters.Add(typeof(MainMenuWidgetFilter)));

        services.AddDataMigration<LayoutInjectionMigrations>();
        services.AddDataMigration<RecipeMigrations>();

        services.AddScoped<IResourceFilterProvider, ResourceFilters>();
        services.AddScoped<IResourceFilterProvider, IconResourceFilter>();

        PerTenantShapeTableManager.ReplaceDefaultShapeTableManager(services);
        services.AddScoped<INavigationProvider, MainMenuNavigationProvider>();

        services.AddScoped<IPermissionProvider, BaseThemeSettingsPermissions>();
        services.AddScoped<INavigationProvider, BaseThemeSettingsAdminMenu>();
    }

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider) =>
        app.UseMiddleware<RemoveBootstrapMiddleware>();
}
