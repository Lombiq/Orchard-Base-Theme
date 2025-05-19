using Lombiq.BaseTheme.Native.Constants;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using CoreResourceNames = Lombiq.BaseTheme.Core.Constants.ResourceNames;

namespace Lombiq.BaseTheme.Native;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) => services.AddResourceFilter(
            builder => builder
                .Always()
                .RegisterStylesheet(ResourceNames.Bootstrap)
                .RegisterFootScript(ResourceNames.Bootstrap)
                .RegisterStylesheet(CoreResourceNames.Helpers),
            FeatureIds.Native);
}
