using Lombiq.BaseTheme.Services;
using Lombiq.HelpfulLibraries.OrchardCore.Shapes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICssClassHolder, CssClassHolder>();
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        services.Configure<MvcOptions>(options => options.Filters.Add(typeof(MainMenuWidgetFilter)));

        services.AddTagHelpers<LayoutZoneTagHelper>();
    }
}
