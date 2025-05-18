using Lombiq.BaseTheme.Constants;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Mvc.Core.Utilities;

namespace Lombiq.BaseTheme.Controllers;

public sealed class AdminControllerA : Controller
{
    // Needed for backwards compatibility.
    public IActionResult Index() =>
        RedirectToActionPermanent(
            nameof(AdminController.Index),
            typeof(AdminController).ControllerName(),
            new { Area = FeatureIds.Core });
}
