using System;
using System.Collections.Generic;
using System.Linq;

namespace Lombiq.BaseTheme.Services;

/// <summary>
/// Service for managing CSS classes to be rendered in the template.
/// </summary>
public interface ICssClassHolder
{
    /// <summary>
    /// Gets the classes associated with the <c>&lt;body&gt;</c> element.
    /// </summary>
    ISet<string> Body { get; }

    /// <summary>
    /// Gets the set that contains the classes that will be attached to the zone called <paramref name="zoneName"/>.
    /// </summary>
    ISet<string> this[string zoneName] { get; }

    /// <summary>
    /// Adds a <paramref name="className"/> to the zone called <paramref name="zoneName"/>.
    /// </summary>
    void AddClassToZone(string zoneName, string className);

    /// <summary>
    /// Returns the set of classes belonging to the zone called <paramref name="zoneName"/>. You can use this to
    /// remove classes if needed.
    /// </summary>
    [Obsolete($"Use {nameof(GetOrAddZoneClasses)} instead.")]
    ISet<string> GetZoneClasses(string zoneName);

    /// <summary>
    /// Returns the set of classes belonging to the zone called <paramref name="zoneName"/>. You can use this to
    /// remove classes if needed.
    /// </summary>
    ISet<string> GetOrAddZoneClasses(string zoneName);
}

public static class CssClassHolderExtensions
{
    /// <summary>
    /// Returns the string you can insert into the zone's template.
    /// </summary>
    public static string ConcatenateZoneClasses(this ICssClassHolder holder, string zoneName, params string[] additionalClasses) =>
        holder
            .GetZoneClasses(zoneName)
            .Concat(additionalClasses)
            .WhereNot(string.IsNullOrEmpty)
            .Join();
}
