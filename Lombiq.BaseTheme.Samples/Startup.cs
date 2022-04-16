using Lombiq.DataTables.Samples.Navigation;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme.Samples;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();

        // This is a sample main menu item. See the "Front-end navigation via the "main" menu" section.
        services.AddScoped<INavigationProvider, AccountNavigationProvider>();

        // This service provides configuration to the ResourceFilterMiddleware.
        services.AddScoped<IResourceFilterProvider, ResourceFilters>();
    }

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider) =>
        // In this theme we inject the style resources using the ResourceFilterMiddleware which needs to be enabled with
        // this extensions. See: https://github.com/Lombiq/Helpful-Libraries/blob/dev/Lombiq.HelpfulLibraries.OrchardCore/Docs/ResourceManagement.md
        app.UseResourceFilters();
}

// NEXT STATION: Services/ResourceFilters.cs
