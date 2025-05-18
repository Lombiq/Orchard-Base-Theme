using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.DisplayManagement;

namespace Lombiq.BaseTheme.Core.ViewModels;

public class BaseThemeSettingsViewModel
{
    public string Icon { get; set; }

    public required bool HideMenu { get; set; }

    [BindNever]
    public IShape Editor { get; set; }
}
