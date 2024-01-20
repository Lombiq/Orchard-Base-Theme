using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Menu.Models;
using OrchardCore.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Services;

public class MainMenuNavigationProvider(
    IHttpContextAccessor hca,
    IStringLocalizer<MainMenuNavigationProvider> stringLocalizer,
    IContentHandleManager contentHandleManager,
    IContentManager contentManager,
    IUrlHelperFactory urlHelperFactory,
    IActionContextAccessor actionContextAccessor) : MainMenuNavigationProviderBase(hca, stringLocalizer)
{
    protected override async Task BuildAsync(NavigationBuilder builder)
    {
        if (await contentHandleManager.GetContentItemIdAsync("alias:main-menu") is not { } id ||
            await contentManager.GetAsync(id) is not { } contentItem ||
            contentItem.As<MenuItemsListPart>() is not { } menuItemsListPart)
        {
            return;
        }

        foreach (var menuItem in menuItemsListPart.MenuItems)
        {
            await AddAsync(builder, menuItem);
        }
    }

    private async Task AddAsync(NavigationBuilder builder, ContentItem menuItem)
    {
        var text = GetTitle(menuItem);

        if (menuItem.As<LinkMenuItemPart>() is { } linkMenuItemPart)
        {
            builder.Add(text, menu => menu.Url(linkMenuItemPart.Url));
        }
        else if (menuItem.As<ContentMenuItemPart>() is { } contentMenuItemPart)
        {
            if (contentMenuItemPart.Content.SelectedContentItem is JObject &&
                contentMenuItemPart.Content.SelectedContentItem.ContentItemIds is JArray contentItemIds)
            {
                var ids = contentItemIds.ToObject<IEnumerable<string>>();
                await AddContentMenuItemPartAsync(builder, text, ids);
            }
        }
        else if (menuItem.As<HtmlMenuItemPart>() is { } htmlMenuItemPart)
        {
            var nodeList = new HtmlParser().ParseFragment($"<div>{htmlMenuItemPart.Html}</div>", contextElement: null!);
            var textContent = string.Concat(nodeList.Select(x => x.Text()));
            builder.Add(new LocalizedString(textContent, textContent), menu => menu.Url("#").LocalNav());
        }
        else if (menuItem.As<MenuItemsListPart>() is { } menuItemsListPart)
        {
            await builder.AddAsync(text, menu =>
                menuItemsListPart.MenuItems.AwaitEachAsync(child => AddAsync(menu, child)));
        }
    }

    private async Task AddContentMenuItemPartAsync(NavigationBuilder builder, LocalizedString text, IEnumerable<string> ids)
    {
        var contentItems = (await contentManager.GetAsync(ids)).AsList();
        var urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);

        if (contentItems.Count == 1)
        {
            var contentItem = contentItems.Single();
            if (string.IsNullOrEmpty(text.Value)) text = GetTitle(contentItem);
            builder.Add(text, menu => menu.Url(urlHelper.DisplayContentItem(contentItem)));
        }
        else
        {
            builder.Add(text, menu =>
            {
                foreach (var contentItem in contentItems)
                {
                    menu.Add(GetTitle(contentItem), child => child.Url(urlHelper.DisplayContentItem(contentItem)));
                }
            });
        }
    }

    private static LocalizedString GetTitle(ContentItem contentItem)
    {
        var displayText = contentItem.DisplayText ?? string.Empty;
        return new LocalizedString(displayText, displayText);
    }
}
