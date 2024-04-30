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

public class ZoneDescriptor
{
    // Elements that may be zones and are landmarks, see https://html-validate.org/rules/unique-landmark.html.
    private static string[] _landmarkElements = ["aside", "footer", "form", "header", "main", "nav", "section"];

    public const string LayoutElementClassName = "layoutElement";
    public const string LeafClassName = LayoutElementClassName + "_leaf";

    public string ZoneName { get; set; }
    public string ElementName { get; set; }
    public bool WrapBody { get; set; }
    public string AriaLabel { get; set; }
    public IDictionary<string, string> Attributes { get; private set; }

    public IEnumerable<ZoneDescriptor> ChildrenBefore { get; set; }
    public IEnumerable<ZoneDescriptor> ChildrenAfter { get; set; }

    public ZoneDescriptor(
        string zoneName,
        string elementName = null,
        bool wrapBody = false,
        string ariaLabel = null,
        IReadOnlyDictionary<string, string> attributes = null)
    {
        ZoneName = zoneName;
        ElementName = elementName;
        WrapBody = wrapBody;
        AriaLabel = ariaLabel;
        Attributes = attributes?.ToDictionary() ?? [];
    }

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

        var attributesFlattened = Attributes.Select(kvp => $"{kvp.Key}=\"{kvp.Value}\"").Join();

        if (WrapBody)
        {
            // If there is no parent then "body" becomes the BEM element, otherwise there already is an element so
            // "Body" becomes a suffix to that.
            var bodyWrapperClass = string.IsNullOrEmpty(parent)
                ? layoutClassName + "__body"
                : layoutClassName + "Body";

            var bodyAttributes = $"class=\"{bodyWrapperClass} {LeafClassName}\" " + attributesFlattened;

            var elementName = "div";

            if (ZoneName == ZoneNames.Content)
            {
                // This improves accessibility by providing a main landmark, see:
                // https://dequeuniversity.com/rules/axe/4.2/bypass?application=axeAPI
                elementName = "main";

                bodyAttributes += GetAriaLabelAttribute(elementName);
            }

            body = new HtmlContentBuilder()
                .AppendHtml(StringHelper.CreateInvariant($"<{elementName} {bodyAttributes}>"))
                .AppendHtml(body)
                .AppendHtml(StringHelper.CreateInvariant($"</{elementName}>"));
        }

        attributesFlattened += GetAriaLabelAttribute(ElementName);

        return new HtmlContentBuilder()
            .AppendHtml(StringHelper.CreateInvariant($"<{ElementName} id=\"{id}\" class=\"{classNames}\" {attributesFlattened}>"))
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

    private string GetAriaLabelAttribute(string elementName)
    {
        // Intentionally no CamelCase word-splitting the ZoneName by default, since that would involve regex for every
        // single page view, for values that one only ever sets once.
        if (!string.IsNullOrEmpty(AriaLabel) || _landmarkElements.Contains(elementName))
        {
            var ariaLabel = string.IsNullOrEmpty(AriaLabel) ? ZoneName : AriaLabel;
            return $"aria-label=\"{ariaLabel}\" ";
        }

        return string.Empty;
    }

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
