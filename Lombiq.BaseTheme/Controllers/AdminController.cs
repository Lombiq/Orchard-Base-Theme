using Lombiq.BaseTheme.Constants;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Mvc.Core.Utilities;
using CoreAdminController = Lombiq.BaseTheme.Core.Controllers.AdminController;

namespace Lombiq.BaseTheme.Controllers;

public sealed class AdminController : Controller
{
    // Needed for backwards compatibility.
    public IActionResult Index() =>
        RedirectToActionPermanent(
            nameof(CoreAdminController.Index),
            typeof(CoreAdminController).ControllerName(),
            new { Area = FeatureIds.Core });
}
