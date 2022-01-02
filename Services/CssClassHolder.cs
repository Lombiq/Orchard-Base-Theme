using System.Collections.Generic;
using System.Linq;

namespace Lombiq.BaseTheme.Services
{
    public class CssClassHolder : ICssClassHolder
    {
        private readonly Dictionary<string, HashSet<string>> _zones = new();

        public HashSet<string> Body { get; } = new();

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

        public HashSet<string> GetZoneClasses(string zoneName) => _zones.GetMaybe(zoneName) ?? new();

        public string ConcatenateZoneClasses(string zoneName, params string[] additionalClasses) =>
            string.Join(" ", GetZoneClasses(zoneName).Concat(additionalClasses));
    }
}
