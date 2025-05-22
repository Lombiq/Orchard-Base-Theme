using Lombiq.BaseTheme.Native.Samples.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;

namespace Lombiq.BaseTheme.Native.Samples;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();

        // This creates an anonymous service that provides configuration to the ResourceFilterMiddleware (which is added
        // by Lombiq.BaseTheme.Core).
        services.AddResourceFilter(
            builder =>
            {
                builder.Always().RegisterStylesheet(ResourceNames.Site);
                builder.WhenContentType("BlogPost").RegisterStylesheet(ResourceNames.BlogPost);
            },
            FeatureIds.NativeSamples);

        // Note that some related features, like frontend navigation providers and admin-side favicon configuration, are
        // not demonstrated here. Check out the non-Sass demos in Lombiq.BaseTheme.Samples project as well.
    }
}
