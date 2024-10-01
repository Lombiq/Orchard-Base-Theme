using Lombiq.BaseTheme.Constants;
using Lombiq.HelpfulLibraries.OrchardCore.Mvc;
using Lombiq.HelpfulLibraries.OrchardCore.ResourceManagement;
using Microsoft.AspNetCore.Http;

namespace Lombiq.BaseTheme.Services;

[ResourceFilterThemeRequirement(FeatureIds.BaseTheme)]
public class ResourceFilters : IResourceFilterProvider
{
    private readonly IHttpContextAccessor _hca;

    public ResourceFilters(IHttpContextAccessor hca) =>
        _hca = hca;

    public void AddResourceFilter(ResourceFilterBuilder builder)
    {
        if (_hca.HttpContext.IsAdminUrl()) return;

        builder.Always().RegisterStylesheet(ResourceNames.Site);
    }
}
