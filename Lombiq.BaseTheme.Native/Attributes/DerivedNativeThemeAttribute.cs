using Lombiq.BaseTheme.Core.Attributes;
using Lombiq.BaseTheme.Native.Constants;
using System;

namespace Lombiq.BaseTheme.Native.Attributes;

/// <summary>
/// Indicates a theme derived from the <see cref="FeatureIds.Native"/> theme.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed class DerivedNativeThemeAttribute : DerivedThemeAttribute
{
    public DerivedNativeThemeAttribute()
        : base(FeatureIds.Native)
    { }
}
