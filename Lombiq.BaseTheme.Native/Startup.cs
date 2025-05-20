using Lombiq.BaseTheme.Native.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace Lombiq.BaseTheme.Native;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) => services.AddResourceFilter(
            builder => builder
                .Always()
                .RegisterBaseThemeCoreResources(),
            FeatureIds.Native);
}
