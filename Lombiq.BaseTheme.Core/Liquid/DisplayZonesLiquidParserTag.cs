using Fluid;
using Fluid.Ast;
using Fluid.Values;
using Lombiq.BaseTheme.Core.Models;
using Lombiq.HelpfulLibraries.Common.Utilities;
using Lombiq.HelpfulLibraries.OrchardCore.Liquid;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Liquid;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Core.Liquid;

public class DisplayZonesLiquidParserTag : ILiquidParserTag, ILiquidFilter
{
    public async ValueTask<Completion> WriteToAsync(
        IReadOnlyList<FilterArgument> argumentsList,
        TextWriter writer,
        TextEncoder encoder,
        TemplateContext context)
    {
        var result = await ProcessAsync(NilValue.Instance, new(), context as LiquidTemplateContext);
        await result.WriteToAsync(writer, encoder, context.CultureInfo);
        return Completion.Normal;
    }

    public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
    {
        if (context?.Services?.GetService<IHtmlLocalizer<ZoneDescriptor>>() is not { } localizer)
        {
            return NilValue.Instance;
        }

        var zoneDescriptors = input switch
        {
            StringValue stringValue => ParseJson(stringValue),
            ArrayValue arrayValue => arrayValue
                .Values
                .SelectMany(item => item switch
                {
                    StringValue stringItem => ParseJson(stringItem),
                    ObjectValue objectItem => ConvertObject(objectItem),
                    _ => [],
                })
                .ToList(),
            ObjectValue objectValue => ConvertObject(objectValue),
            _ => [],
        };

        if (zoneDescriptors is not { Count: > 0 })
        {
            zoneDescriptors = ZoneDescriptor.GetDefaultZoneDescriptors(localizer);
        }

        var html = (await ZoneDescriptor.RenderZonesAsync(context.Services, zoneDescriptors)).Html();
        return new StringValue(html, encode: false);
    }

    private static IList<ZoneDescriptor> ParseJson(StringValue stringValue)
    {
        if (!JsonHelpers.TryParse(stringValue.ToStringValue(), out var json)) return [];

        if (json is JsonArray)
        {
            return json.TryParse<IList<ZoneDescriptor>>(out var jsonDescriptors) ? jsonDescriptors : [];
        }

        return json.TryParse<ZoneDescriptor>(out var jsonDescriptor) ? [jsonDescriptor] : [];
    }

    private static IList<ZoneDescriptor> ConvertObject(ObjectValue objectValue) =>
        objectValue.Value is ZoneDescriptor zoneDescriptor ? [zoneDescriptor] : [];
}
