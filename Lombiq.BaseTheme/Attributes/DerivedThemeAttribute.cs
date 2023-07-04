using Lombiq.BaseTheme.Constants;
using OrchardCore.DisplayManagement.Manifest;
using OrchardCore.ResourceManagement;
using System;
using System.Collections.Generic;

namespace Lombiq.BaseTheme.Attributes;

/// <summary>
/// Indicates a theme derived from <c>Lombiq.BaseTheme</c>.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed class DerivedThemeAttribute : ThemeAttribute
{
    public IEnumerable<LinkEntry> Links { get; set; }
    public string Favicon { get; set; }

    public DerivedThemeAttribute() =>
        BaseTheme = FeatureIds.BaseTheme;
}
