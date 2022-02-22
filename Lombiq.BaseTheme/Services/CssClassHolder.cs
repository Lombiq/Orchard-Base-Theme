using System.Collections.Generic;

namespace Lombiq.BaseTheme.Services
{
    public class CssClassHolder : ICssClassHolder
    {
        private readonly Dictionary<string, HashSet<string>> _zones = new();

        public ISet<string> Body { get; } = new HashSet<string>();

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

        public ISet<string> GetZoneClasses(string zoneName) => _zones.GetMaybe(zoneName) ?? new();
    }
}
