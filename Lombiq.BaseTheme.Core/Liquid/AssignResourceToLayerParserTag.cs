using Fluid;
using Fluid.Ast;
using Lombiq.BaseTheme.Core.Services;
using Lombiq.HelpfulLibraries.OrchardCore.Liquid;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Core.Liquid;

public class AssignResourceToLayerParserTag : ILiquidParserTag
{
    private readonly IStyleToLayerMappingAccessor _accessor;

    public AssignResourceToLayerParserTag(IStyleToLayerMappingAccessor accessor) =>
        _accessor = accessor;

    public async ValueTask<Completion> WriteToAsync(
        IReadOnlyList<FilterArgument> argumentsList,
        TextWriter writer,
        TextEncoder encoder,
        TemplateContext context)
    {
        var resource = await EvaluateAsync(argumentsList[0], context);
        var layer = await EvaluateAsync(argumentsList, "to", context);

        _accessor.Mapping[resource] = layer;

        return Completion.Normal;
    }

    private static async Task<string> EvaluateAsync(
        IReadOnlyList<FilterArgument> argumentsList,
        string name,
        TemplateContext context)
    {
        var argument = argumentsList.First(argument => argument.Name == name);
        return (await argument.Expression.EvaluateAsync(context)).ToStringValue().Trim();
    }

    private static async Task<string> EvaluateAsync(FilterArgument argument, TemplateContext context) =>
        (await argument.Expression.EvaluateAsync(context)).ToStringValue().Trim();
}
