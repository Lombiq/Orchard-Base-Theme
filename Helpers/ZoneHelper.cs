using GraphQL;
using Microsoft.AspNetCore.Html;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Razor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Helpers
{
    public static class ZoneHelper
    {
        public static async Task<IHtmlContent> DisplayZoneAsync<TModel>(
            RazorPage<TModel> page,
            string zoneName)
        {
            if (page.Model is not IDictionary<string, object> model ||
                model.GetMaybe(zoneName) is not IShape zone)
            {
                return new HtmlString(string.Empty);
            }

            var id = zoneName.ToCamelCase();

            return new HtmlContentBuilder()
                .AppendHtml(FormattableString.Invariant($"<div id=\"{id}\">"))
                .AppendHtml(await page.DisplayAsync(zone))
                .AppendHtml("</div>");
        }
    }
}
