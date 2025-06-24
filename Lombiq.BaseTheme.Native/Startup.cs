using Lombiq.BaseTheme.Core.Services;
using Lombiq.BaseTheme.Native.Constants;
using Lombiq.BaseTheme.Native.Services;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

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

        services.AddResourceManagementConfiguration<ResourceManagementOptionsConfiguration>();

        services.AddScoped<IBodyClassProvider, BreakpointBodyClassProvider>();
    }
}
