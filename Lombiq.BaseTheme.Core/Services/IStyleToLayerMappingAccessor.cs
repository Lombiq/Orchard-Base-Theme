using System.Collections.Generic;

namespace Lombiq.BaseTheme.Core.Services;

/// <summary>
/// Contains the mapping used by <see cref="LayerAwareResourceManager"/>.
/// </summary>
public interface IStyleToLayerMappingAccessor
{
    /// <summary>
    /// Gets the mapping for <see cref="LayerAwareResourceManager"/>. The key is the style resource name, the value is
    /// the CSS layer name. The resources with a key here are imported using the <c>@import 'url' layer(layer-name)</c>
    /// directive inside an inline style element instead of the usual link.
    /// </summary>
    IDictionary<string, string> Mapping { get; }
}
