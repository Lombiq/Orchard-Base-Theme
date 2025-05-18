using Lombiq.BaseTheme.Constants;
using OrchardCore.DisplayManagement.Manifest;
using OrchardCore.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Lombiq.BaseTheme.Attributes;

/// <summary>
/// Indicates a theme derived from <c>Lombiq.BaseTheme</c>.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
[SuppressMessage("Performance", "CA1813:Avoid unsealed attributes", Justification = "Needed to support Sass and CSS base themes.")]
public class DerivedThemeAttribute : ThemeAttribute
{
    public string LinksJson { get; set; }
    public string Favicon { get; set; }

    public IEnumerable<LinkEntry> Links => LinksJson == null
        ? []
        : JsonSerializer.Deserialize<IEnumerable<LinkEntry>>(LinksJson);

    public DerivedThemeAttribute(string baseTheme = "Lombiq.BaseTheme") =>
        BaseTheme = baseTheme;
}
