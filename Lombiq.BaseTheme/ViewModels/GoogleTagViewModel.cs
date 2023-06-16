using OrchardCore.DisplayManagement.Views;

namespace Lombiq.BaseTheme.ViewModels;

public class GoogleTagViewModel : ShapeViewModel
{
    public string GoogleTagPropertyId { get; set; }
    public string CookieDomain { get; set; }
    public GoogleTagViewModel() => Metadata.Type = "GoogleTag";
}
