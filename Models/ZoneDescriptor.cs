﻿using GraphQL;
using Lombiq.BaseTheme.Services;
using Microsoft.AspNetCore.Html;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Razor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Models
{
    public class ZoneDescriptor
    {
        public string ZoneName { get; set; }
        public string ElementName { get; set; }
        public bool WrapBody { get; set; }

        public IEnumerable<ZoneDescriptor> ChildrenBefore { get; init; }
        public IEnumerable<ZoneDescriptor> ChildrenAfter { get; init; }

        public ZoneDescriptor(string zoneName = null, string elementName = null, bool wrapBody = false)
        {
            ZoneName = zoneName;
            ElementName = elementName;
            WrapBody = wrapBody;
        }

        public async Task<IHtmlContent> DisplayZoneAsync<TModel>(
            ICssClassHolder classHolder,
            RazorPage<TModel> page,
            string parent)
        {
            if (page.Model is not IDictionary<string, object> model ||
                model.GetMaybe(ZoneName) is not IShape zone)
            {
                return new HtmlString(string.Empty);
            }

            ElementName ??= "div";

            // The zone name should already be PascalCase.
            var id = ZoneName.ToCamelCase();
            var layoutClassName = string.IsNullOrEmpty(parent)
                ? "layout" + ZoneName
                : FormattableString.Invariant($"layout{parent}__{id}");

            var classNames = classHolder.ConcatenateZoneClasses(ZoneName, layoutClassName);

            return new HtmlContentBuilder()
                .AppendHtml(FormattableString.Invariant($"<{ElementName} id=\"{id}\" classes=\"{classNames}\">"))
                .AppendHtml(await ConcatenateAsync(classHolder, page, ChildrenBefore, parent))
                .AppendHtml(await page.DisplayAsync(zone))
                .AppendHtml(await ConcatenateAsync(classHolder, page, ChildrenAfter, parent))
                .AppendHtml(FormattableString.Invariant($"</{ElementName}>"));
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
                : FormattableString.Invariant($"{parent}__{zoneName}");

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
}