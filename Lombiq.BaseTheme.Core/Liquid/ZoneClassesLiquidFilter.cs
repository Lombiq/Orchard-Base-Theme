using Fluid;
using Fluid.Values;
using Lombiq.BaseTheme.Core.Constants;
using Lombiq.BaseTheme.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Liquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Core.Liquid;

public class ZoneClassesLiquidFilter : ILiquidFilter
{
    public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
    {
        var zoneName = arguments.HasNamed("zone") ? arguments["zone"].ToStringValue() : ZoneNames.Content;
        var remove = arguments.HasNamed("remove") && arguments["remove"].ToBooleanValue();

        var classHolder = context.Services.GetRequiredService<ICssClassHolder>();
        var zoneClasses = classHolder.GetZoneClasses(zoneName);

#pragma warning disable IDE0010 // Populate switch. We only want to handle relevant types.
        switch (input?.Type)
#pragma warning restore IDE0010 // Populate switch.
        {
            case FluidValues.String:
                AddClasses(new([input]), zoneClasses, remove);
                break;
            case FluidValues.Array:
                AddClasses((ArrayValue)input, zoneClasses, remove);
                break;
            default:
                AddClasses(new([new StringValue(input?.ToStringValue())]), zoneClasses, remove);
                break;
        }

        return NilValue.Instance;
    }

    private static void AddClasses(ArrayValue array, ISet<string> zoneClasses, bool remove)
    {
        var classes = array
            .Values
            .Where(item => !item.IsNil())
            .SelectWhere(item => item.ToStringValue())
            .SelectMany(item => item.SplitByCommas())
            .SelectMany(item => item.Split().WhereNot(string.IsNullOrEmpty));

        foreach (var item in classes)
        {
            if (remove)
            {
                zoneClasses.Remove(item);
            }
            else
            {
                zoneClasses.Add(item);
            }
        }
    }
}
