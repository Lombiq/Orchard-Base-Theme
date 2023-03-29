using System.Collections.Generic;

namespace Lombiq.BaseTheme.Services;

public class CssClassHolder : ICssClassHolder
{
    private readonly Dictionary<string, HashSet<string>> _classesByZones = new();

    public ISet<string> Body { get; } = new HashSet<string>();

    public ISet<string> this[string zoneName] => GetZoneClasses(zoneName);

    public void AddClassToZone(string zoneName, string className) =>
        GetZoneClasses(zoneName).Add(className);

    public ISet<string> GetZoneClasses(string zoneName)
    {
        if (_classesByZones.TryGetValue(zoneName, out var classes)) return classes;

        classes = new();
        _classesByZones[zoneName] = classes;
        return classes;
    }
}
