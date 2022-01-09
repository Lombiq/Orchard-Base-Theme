using System.Collections.Generic;
using System.Linq;

namespace Lombiq.BaseTheme.Services
{
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
        /// Adds a <paramref name="className"/> to the zone called <paramref name="zoneName"/>.
        /// </summary>
        void AddClassToZone(string zoneName, string className);

        /// <summary>
        /// Returns the set of classes belonging to the zone called <paramref name="zoneName"/>. You can use this to
        /// remove classes if needed.
        /// </summary>
        ISet<string> GetZoneClasses(string zoneName);
    }

    public static class CssClassHolderExtensions
    {
        /// <summary>
        /// Returns the string you can insert into the zone's template.
        /// </summary>
        public static string ConcatenateZoneClasses(this ICssClassHolder holder, string zoneName, params string[] additionalClasses) =>
            string.Join(" ", holder.GetZoneClasses(zoneName).Concat(additionalClasses));
    }
}
