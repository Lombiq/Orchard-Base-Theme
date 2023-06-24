using OrchardCore.Media.Fields;

namespace Lombiq.BaseTheme.Models;

public class BaseThemeSettings
{
    public MediaField Thumbnail { get; set; } = new();
}
