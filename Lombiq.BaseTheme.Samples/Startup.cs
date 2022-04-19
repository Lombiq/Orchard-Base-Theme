using Lombiq.DataTables.Samples.Navigation;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using static Lombiq.BaseTheme.Samples.Constants.FeatureIds;

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

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        // In this theme we inject the style resources using the ResourceFilterMiddleware which needs to be enabled with
        // this extension. See: https://github.com/Lombiq/Helpful-Libraries/blob/dev/Lombiq.HelpfulLibraries.OrchardCore/Docs/ResourceManagement.md
        app.UseResourceFilters();

        // Certain browsers expect the site's favicon to be in the default location and try to load it from anyway. If
        // you add a <link> element you will still get an unnecessary lost GET request because of that, and of course it
        // contributes to the page size. It's better to change what ~/favicon.ico means instead.
        // See https://orcharddojo.net/blog/how-to-add-a-favicon-under-favicon-ico-in-orchard-core-orchard-nuggets
        app.Map("/favicon.ico", appBuilder => appBuilder.Run(context =>
        {
            context.Response.Redirect($"/{BaseThemeSamples}/icons/favicon.ico", permanent: true);
            return Task.CompletedTask;
        }));
    }
}

// NEXT STATION: Services/ResourceFilters.cs
