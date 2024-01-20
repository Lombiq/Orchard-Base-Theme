using GraphQL;
using Lombiq.BaseTheme.Constants;
using Lombiq.BaseTheme.Services;
using Lombiq.HelpfulLibraries.Common.Utilities;
using Microsoft.AspNetCore.Html;
using OrchardCore.DisplayManagement.Razor;
using OrchardCore.DisplayManagement.Zones;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Models;

public class ZoneDescriptor(string zoneName = null, string elementName = null, bool wrapBody = false)
{
    public const string LayoutElementClassName = "layoutElement";
    public const string LeafClassName = LayoutElementClassName + "_leaf";

    public string ZoneName { get; set; } = zoneName;
    public string ElementName { get; set; } = elementName;
    public bool WrapBody { get; set; } = wrapBody;

    public IEnumerable<ZoneDescriptor> ChildrenBefore { get; set; }
    public IEnumerable<ZoneDescriptor> ChildrenAfter { get; set; }

    public async Task<IHtmlContent> DisplayZoneAsync<TModel>(
        ICssClassHolder classHolder,
        RazorPage<TModel> page,
        string parent)
    {
        if (page.Model is not IZoneHolding model ||
            model.Zones[ZoneName] is not { } zone)
        {
            return new HtmlString(string.Empty);
        }

        ElementName ??= "div";

        // The zone name should already be PascalCase.
        var id = ZoneName.ToCamelCase();
        var layoutClassName = string.IsNullOrEmpty(parent)
            ? "layout" + ZoneName
            : StringHelper.CreateInvariant($"layout{parent}__{id}");

        var classNames = classHolder.ConcatenateZoneClasses(
            ZoneName,
            layoutClassName,
            LayoutElementClassName,
            ChildrenBefore?.Any() != true || ChildrenAfter?.Any() != true ? LeafClassName : null);

        var body = await page.DisplayAsync(zone);
        if (WrapBody)
        {
            // If there is no parent then "body" becomes the BEM element, otherwise there already is an element so
            // "Body" becomes a suffix to that.
            var bodyWrapperClass = string.IsNullOrEmpty(parent)
                ? layoutClassName + "__body"
                : layoutClassName + "Body";

            // This improves accessibility by providing a main landmark, see:
            // https://dequeuniversity.com/rules/axe/4.2/bypass?application=axeAPI
            var htmlElementName = ZoneName == ZoneNames.Content ? "main" : "div";

            body = new HtmlContentBuilder()
                .AppendHtml(StringHelper.CreateInvariant($"<{htmlElementName} class=\"{bodyWrapperClass} {LeafClassName}\">"))
                .AppendHtml(body)
                .AppendHtml(StringHelper.CreateInvariant($"</{htmlElementName}>"));
        }

        return new HtmlContentBuilder()
            .AppendHtml(StringHelper.CreateInvariant($"<{ElementName} id=\"{id}\" class=\"{classNames}\">"))
            .AppendHtml(await ConcatenateAsync(classHolder, page, ChildrenBefore, parent))
            .AppendHtml(body)
            .AppendHtml(await ConcatenateAsync(classHolder, page, ChildrenAfter, parent))
            .AppendHtml(StringHelper.CreateInvariant($"</{ElementName}>"));
    }

    private Task<IHtmlContent> ConcatenateAsync<TModel>(
        ICssClassHolder classHolder,
        RazorPage<TModel> page,
        IEnumerable<ZoneDescriptor> zoneDescriptors,
        string parent) =>
        zoneDescriptors == null
            ? Task.FromResult<IHtmlContent>(new HtmlString(string.Empty))
            : ConcatenateInnerAsync(classHolder, page, zoneDescriptors, ZoneName, parent);

    private static async Task<IHtmlContent> ConcatenateInnerAsync<TModel>(
        ICssClassHolder classHolder,
        RazorPage<TModel> page,
        IEnumerable<ZoneDescriptor> zoneDescriptors,
        string zoneName,
        string parent)
    {
        var builder = new HtmlContentBuilder();

        var newParent = string.IsNullOrEmpty(parent)
            ? zoneName
            : StringHelper.CreateInvariant($"{parent}__{zoneName}");

        foreach (var zoneDescriptor in zoneDescriptors)
        {
            _ = builder.AppendHtml(await zoneDescriptor.DisplayZoneAsync(classHolder, page, newParent));
        }

        return builder;
    }

    public static Task<IHtmlContent> DisplayZonesAsync<TModel>(
        ICssClassHolder classHolder,
        RazorPage<TModel> page,
        IEnumerable<ZoneDescriptor> zoneDescriptors) =>
        ConcatenateInnerAsync(classHolder, page, zoneDescriptors, zoneName: null, parent: null);
}
