using Lombiq.BaseTheme.Samples.Migrations;
using Lombiq.DataTables.Samples.Navigation;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using System;

namespace Lombiq.BaseTheme.Samples;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();

        // This is a sample main menu item. See the "Front-end navigation via the "main" menu" section.
        services.AddNavigationProvider<AccountNavigationProvider>();

        // This service provides configuration to the ResourceFilterMiddleware.
        services.AddResourceFilter<ResourceFilters>();

        // The recipe migration is used to add the media items and Base Theme settings required for the correct favicon.
        services.AddDataMigration<RecipeMigrations>();
    }
}

// NEXT STATION: Services/ResourceFilters.cs
