using System;
using System.Collections.Generic;

namespace Lombiq.BaseTheme.Services;

public class CssClassHolder : ICssClassHolder
{
    private readonly Dictionary<string, HashSet<string>> _zones = new();

    public ISet<string> Body { get; } = new HashSet<string>();

    public ISet<string> this[string zoneName] => GetOrAddZoneClasses(zoneName);

    public void AddClassToZone(string zoneName, string className)
    {
        var classes = _zones.GetMaybe(zoneName);

        if (classes == null)
        {
            classes = new();
            _zones[zoneName] = classes;
        }

        classes.Add(className);
    }

    [Obsolete($"Use {nameof(GetOrAddZoneClasses)} instead.")]
    public ISet<string> GetZoneClasses(string zoneName) => GetOrAddZoneClasses(zoneName);

    public ISet<string> GetOrAddZoneClasses(string zoneName)
    {
        if (_zones.TryGetValue(zoneName, out var zone)) return zone;

        zone = new HashSet<string>();
        _zones[zoneName] = zone;
        return zone;
    }
}
