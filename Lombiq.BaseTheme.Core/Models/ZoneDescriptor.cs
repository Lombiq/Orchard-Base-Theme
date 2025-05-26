using AngleSharp.Dom;
using GraphQL;
using Lombiq.BaseTheme.Core.Constants;
using Lombiq.BaseTheme.Core.Services;
using Lombiq.HelpfulLibraries.Common.Utilities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.DisplayManagement.Razor;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using static Lombiq.BaseTheme.Core.Constants.ZoneNames;

namespace Lombiq.BaseTheme.Core.Models;

public class ZoneDescriptor
{
    public const string LayoutElementClassName = "layoutElement";
    public const string LeafClassName = LayoutElementClassName + "_leaf";

    // Elements that may be zones and are landmarks, see https://html-validate.org/rules/unique-landmark.html.
    private static readonly string[] _landmarkElements =
        [TagNames.Aside, TagNames.Footer, TagNames.Form, TagNames.Header, TagNames.Main, TagNames.Nav, TagNames.Section];

    public string ZoneName { get; set; }
    public string ElementName { get; set; }
    public bool WrapBody { get; set; }
    public LocalizedHtmlString AriaLabel { get; set; }
    public IDictionary<string, string> Attributes { get; private set; }

    public IEnumerable<ZoneDescriptor> ChildrenBefore { get; set; }
    public IEnumerable<ZoneDescriptor> ChildrenAfter { get; set; }

    public ZoneDescriptor(
        string zoneName,
        string elementName = null,
        bool wrapBody = false,
        LocalizedHtmlString ariaLabel = null,
        IReadOnlyDictionary<string, string> attributes = null)
    {
        ZoneName = zoneName;
        ElementName = elementName;
        WrapBody = wrapBody;
        AriaLabel = ariaLabel;
        Attributes = attributes?.ToDictionary() ?? [];
    }

    [Obsolete("Use the other overload.")]
    public Task<IHtmlContent> DisplayZoneAsync<TModel>(
        ICssClassHolder classHolder,
        RazorPage<TModel> page,
        string parent) =>
        DisplayZoneAsync(page.Context.RequestServices, parent);

    public async Task<IHtmlContent> DisplayZoneAsync(IServiceProvider serviceProvider, string parent)
    {
        if (serviceProvider.GetService<ILayoutAccessor>() is not { } layourAccessor ||
            await layourAccessor.GetLayoutAsync() is not { } zoneHolding ||
            zoneHolding.Zones[ZoneName] is not { } zone)
        {
            return new HtmlString(string.Empty);
        }

        ElementName ??= TagNames.Div;

        // The zone name should already be PascalCase.
        var id = ZoneName.ToCamelCase();
        var layoutClassName = string.IsNullOrEmpty(parent)
            ? "layout" + ZoneName
            : StringHelper.CreateInvariant($"layout{parent}__{id}");

        var classHolder = serviceProvider.GetRequiredService<ICssClassHolder>();
        var classNames = classHolder.ConcatenateZoneClasses(
            ZoneName,
            layoutClassName,
            LayoutElementClassName,
            ChildrenBefore?.Any() != true || ChildrenAfter?.Any() != true ? LeafClassName : null);

        var displayHelper = serviceProvider.GetRequiredService<IDisplayHelper>();
        var body = await displayHelper.ShapeExecuteAsync(zone);

        var attributesFlattened = Attributes.Select(attribute => $"{attribute.Key}=\"{attribute.Value}\"").Join();

        if (WrapBody)
        {
            // If there is no parent then "body" becomes the BEM element, otherwise there already is an element so
            // "Body" becomes a suffix to that.
            var bodyWrapperClass = string.IsNullOrEmpty(parent)
                ? layoutClassName + "__body"
                : layoutClassName + "Body";

            var bodyAttributes = $"class=\"{bodyWrapperClass} {LayoutElementClassName} {LeafClassName}\" " + attributesFlattened;

            var elementName = TagNames.Div;

            if (ZoneName == Content)
            {
                // This improves accessibility by providing a main landmark, see:
                // https://dequeuniversity.com/rules/axe/4.2/bypass?application=axeAPI
                elementName = TagNames.Main;

                bodyAttributes += GetAriaLabelAttribute(elementName);
            }

            body = new HtmlContentBuilder()
                .AppendHtml(StringHelper.CreateInvariant($"<{elementName} {bodyAttributes}>"))
                .AppendHtml(body)
                .AppendHtml(StringHelper.CreateInvariant($"</{elementName}>"));
        }

        attributesFlattened += GetAriaLabelAttribute(ElementName);

        Task<IHtmlContent> ConcatenateChildrenAsync(IEnumerable<ZoneDescriptor> zoneDescriptors, string parent) =>
            zoneDescriptors == null
                ? Task.FromResult<IHtmlContent>(new HtmlString(string.Empty))
                : ConcatenateInnerAsync(serviceProvider, zoneDescriptors, ZoneName, parent);

        return new HtmlContentBuilder()
            .AppendHtml(StringHelper.CreateInvariant($"<{ElementName} id=\"{id}\" class=\"{classNames}\" {attributesFlattened}>"))
            .AppendHtml(await ConcatenateChildrenAsync(ChildrenBefore, parent))
            .AppendHtml(body)
            .AppendHtml(await ConcatenateChildrenAsync(ChildrenAfter, parent))
            .AppendHtml(StringHelper.CreateInvariant($"</{ElementName}>"));
    }

    private string GetAriaLabelAttribute(string elementName)
    {
        // Intentionally no CamelCase word-splitting the ZoneName by default, since that would involve regex for every
        // single page view, for values that one only ever set once.
        if (AriaLabel != null || _landmarkElements.Contains(elementName))
        {
            return $"aria-label=\"{(AriaLabel ?? new LocalizedHtmlString(ZoneName, ZoneName)).Value}\" ";
        }

        return string.Empty;
    }

    private static Task<IHtmlContent> ConcatenateFromRootAsync(
        IServiceProvider serviceProvider, IEnumerable<ZoneDescriptor> zoneDescriptors) =>
        ConcatenateInnerAsync(serviceProvider, zoneDescriptors, zoneName: null, parent: null);

    private static async Task<IHtmlContent> ConcatenateInnerAsync(
        IServiceProvider serviceProvider,
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
            _ = builder.AppendHtml(await zoneDescriptor.DisplayZoneAsync(serviceProvider, newParent));
        }

        return builder;
    }

    internal static Task<IHtmlContent> RenderZonesAsync(
        IServiceProvider serviceProvider,
        IEnumerable<ZoneDescriptor> zoneDescriptors = null)
    {
        zoneDescriptors ??= GetDefaultZoneDescriptors(
            serviceProvider.GetRequiredService<IHtmlLocalizer<ZoneDescriptor>>());

        return ConcatenateFromRootAsync(serviceProvider, zoneDescriptors);
    }

    [Obsolete($"Use the {nameof(RenderZonesAsync)} method instead.")]
    public static Task<IHtmlContent> DisplayZonesAsync<TModel>(
        ICssClassHolder classHolder,
        RazorPage<TModel> page,
        IEnumerable<ZoneDescriptor> zoneDescriptors) =>
        ConcatenateFromRootAsync(page.Context.RequestServices, zoneDescriptors);

    [SuppressMessage(
        "StyleCop.CSharp.NamingRules",
        "SA1313:Parameter names should begin with lower-case letter",
        Justification = "The localizer is conventionally named T.")]
    public static IList<ZoneDescriptor> GetDefaultZoneDescriptors(IHtmlLocalizer<ZoneDescriptor> T) =>
    [
        new(Header, wrapBody: true, elementName: TagNames.Header, ariaLabel: T["Site"])
        {
            ChildrenBefore = [new(Banner, elementName: TagNames.Section)],
            ChildrenAfter =
            [
                new(ZoneNames.Navigation, elementName: TagNames.Nav, ariaLabel: T["Main"])
            ],
        },
        new(BeforeMain, elementName: TagNames.Section, ariaLabel: T["Before Main"]),
        new(Featured, elementName: TagNames.Section),
        new(Content, wrapBody: true, elementName: TagNames.Section)
        {
            ChildrenBefore =
            [
                new(AsideFirst, elementName: TagNames.Aside, ariaLabel: T["Aside First"]),
                new(Messages, elementName: TagNames.Section),
                new(BeforeContent, elementName: TagNames.Section, ariaLabel: T["Before Content"])
            ],
            ChildrenAfter =
            [
                new(AfterContent, elementName: TagNames.Section, ariaLabel: T["After Content"]),
                new(AsideSecond, elementName: TagNames.Aside, ariaLabel: T["Aside Second"])
            ],
        },
        new(AfterMain, elementName: TagNames.Section, ariaLabel: T["After Main"]),
        new(Footer, elementName: TagNames.Footer, ariaLabel: T["Site"])
    ];
}
