using Lombiq.BaseTheme.Constants;
using Lombiq.BaseTheme.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        services.AddResourceFilter(
            builder => builder.Always().RegisterStylesheet(ResourceNames.Site),
            FeatureIds.BaseTheme);
        services.Decorate<IResourceManager, ResourceManagerDecorator>();
    }
}
