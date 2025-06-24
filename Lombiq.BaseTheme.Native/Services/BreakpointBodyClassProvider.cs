using Lombiq.BaseTheme.Core.Services;
using Microsoft.AspNetCore.Http;
using OrchardCore.Liquid;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lombiq.BaseTheme.Native.Services;

public class BreakpointBodyClassProvider : SyncBodyClassProvider
{
    private const string CookieName = "BaseThemeSize";
    private const string Prefix = "breakpoint-";

    // Based on the official default Bootstrap breakpoint sizes, these won't change.
    private static readonly IDictionary<string, int> _sizes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        ["xs"] = 0,
        ["sm"] = 576,
        ["md"] = 768,
        ["lg"] = 992,
        ["xl"] = 1200,
        ["xxl"] = 1400,
    };

    private readonly IHttpContextAccessor _hca;

    public BreakpointBodyClassProvider(IHttpContextAccessor hca) =>
        _hca = hca;

    protected override IEnumerable<string> GetClasses(LiquidTemplateContext context)
    {
        var size = _sizes.First().Key;

        if (_hca.HttpContext?.Request.Cookies.TryGetValue(CookieName, out var cookieSize) == true &&
            _sizes.ContainsKey(cookieSize))
        {
            size = _sizes.Single(pair => pair.Key.EqualsOrdinalIgnoreCase(cookieSize)).Key;
        }

        var results = new List<string>
        {
            $"{Prefix}{size}",
            size is "xs" or "sm" ? $"{Prefix}small" : $"{Prefix}big",
        };

        var sizes = _sizes.Keys.ToList();
        var index = sizes.IndexOf(size);
        var start = sizes[..(index + 1)];
        var end = sizes[index..];
        results.AddRange(start.CartesianProduct(end).Select(pair => $"{Prefix}{pair.Left}-{pair.Right}"));

        return results;
    }
}
