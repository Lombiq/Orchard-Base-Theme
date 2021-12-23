using Lombiq.BaseTheme.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using System;

namespace Lombiq.BaseTheme
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services) =>
            services.AddScoped<ICssClassHolder, CssClassHolder>();

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // Method left empty, for future use.
        }
    }
}
