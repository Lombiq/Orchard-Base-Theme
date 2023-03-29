using System.Collections.Generic;

namespace Lombiq.BaseTheme.Services;

public class CssClassHolder : ICssClassHolder
{
    private readonly Dictionary<string, HashSet<string>> _classesByZoneNames = new();

    public ISet<string> Body { get; } = new HashSet<string>();

    public ISet<string> this[string zoneName] => GetZoneClasses(zoneName);

    public void AddClassToZone(string zoneName, string className)
    {
        var classes = _classesByZoneNames.GetMaybe(zoneName);

        if (classes == null)
        {
            classes = new();
            _classesByZoneNames[zoneName] = classes;
        }

        classes.Add(className);
    }

    public ISet<string> GetZoneClasses(string zoneName)
    {
        if (_classesByZoneNames.TryGetValue(zoneName, out var classes)) return classes;

        classes = new HashSet<string>();
        _classesByZoneNames[zoneName] = classes;
        return classes;
    }
}
