using System;
using System.Collections.Generic;

namespace Lombiq.BaseTheme.Core.Services;

public class StyleToLayerMappingAccessor : IStyleToLayerMappingAccessor
{
    public IDictionary<string, string> Mapping { get; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
}
