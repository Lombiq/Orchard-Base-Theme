using GraphQL;
using Microsoft.AspNetCore.Html;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Razor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        HashSet<string> Body { get; }

        /// <summary>
        /// Adds a <paramref name="className"/> to the zone called <paramref name="zoneName"/>.
        /// </summary>
        void AddClassToZone(string zoneName, string className);

        /// <summary>
        /// Returns the set of classes belonging to the zone called <paramref name="zoneName"/>. You can use this to
        /// remove classes if needed.
        /// </summary>
        HashSet<string> GetZoneClasses(string zoneName);

        /// <summary>
        /// Returns the string you can insert into the zone's template.
        /// </summary>
        string ConcatenateZoneClasses(string zoneName, params string[] additionalClasses);
    }
}
