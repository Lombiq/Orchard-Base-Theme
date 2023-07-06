using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.DisplayManagement;

namespace Lombiq.BaseTheme.ViewModels;

public class BaseThemeSettingsViewModel
{
    public string Icon { get; set; }
    public bool HideMenu { get; set; }

    [BindNever]
    public IShape Editor { get; set; }
}
