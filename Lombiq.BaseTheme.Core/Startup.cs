using Fluid;
using Fluid.Values;
using Lombiq.BaseTheme.Core.Liquid;
using Lombiq.BaseTheme.Core.Migrations;
using Lombiq.BaseTheme.Core.Navigation;
using Lombiq.BaseTheme.Core.Permissions;
using Lombiq.BaseTheme.Core.Services;
using Lombiq.HelpfulLibraries.AspNetCore.Extensions;
using Lombiq.HelpfulLibraries.OrchardCore.Liquid;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Lombiq.HelpfulLibraries.OrchardCore.Shapes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        services.AddResourceManagementConfiguration<ResourceManagementOptionsConfiguration>();

        services.AddDisplayChildrenLiquidFilter();
        services.AddLiquidFilter<ZoneClassesLiquidFilter>("zone-classes");
        services.AddLiquidFilter<DisplayZonesLiquidParserTag>("display-zones");
        services.AddLiquidParserTag<DisplayZonesLiquidParserTag>("display-zones");
        services.AddLiquidParserTag<AssignResourceToLayerParserTag>("assign-resource");

        services.Configure<TemplateOptions>(options =>
        {
            options.MemberAccessStrategy.Register<CssClassHolder>();
            options.Scope.SetValue("Theme", new ObjectValue(new LiquidBaseThemeAccessor()));

            options.RegisterLiquidPropertyAccessor<LiquidBaseThemeAccessor, IEnumerable<string>>("ZoneCss", (zone, context) =>
            {
                var classHolder = context.Services.GetRequiredService<ICssClassHolder>();
                return Task.FromResult(classHolder.GetZoneClasses(zone).AsEnumerable());
            });
        });

        services.Decorate<IResourceManager, LayerAwareResourceManager>();
        services.AddScoped<IStyleToLayerMappingAccessor, StyleToLayerMappingAccessor>();
    }

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider) =>
        app.UseResourceFilters();
}
