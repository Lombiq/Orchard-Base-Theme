using Lombiq.BaseTheme.Native.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme.Native;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddResourceFilter(
            builder => builder
                .Always()
                .RegisterBaseThemeCoreResources()
                .RegisterStylesheet(ResourceNames.General)
                .RegisterHeadScript(ResourceNames.GridBreakpoints),
            FeatureIds.Native);

        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
    }
}
