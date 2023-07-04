using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Menu.Models;
using OrchardCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Services;

public class MainMenuNavigationProvider : MainMenuNavigationProviderBase
{
    private readonly IContentHandleManager _contentHandleManager;
    private readonly IContentManager _contentManager;

    private readonly Lazy<IUrlHelper> _urlHelperLazy;

    public MainMenuNavigationProvider(
        IHttpContextAccessor hca,
        IStringLocalizer<MainMenuNavigationProvider> stringLocalizer,
        IContentHandleManager contentHandleManager,
        IContentManager contentManager,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor)
        : base(hca, stringLocalizer)
    {
        _contentHandleManager = contentHandleManager;
        _contentManager = contentManager;

        _urlHelperLazy = new(() => urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!));
    }

    protected override async Task BuildAsync(NavigationBuilder builder)
    {
        if (await _contentHandleManager.GetContentItemIdAsync("alias:main-menu") is not { } id ||
            await _contentManager.GetAsync(id) is not { } contentItem ||
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
            builder.Add(text, menu => menu.Url(GetUrl(linkMenuItemPart.Url)));
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
            var url = string.IsNullOrEmpty(htmlMenuItemPart.Url) ? "#" : GetUrl(htmlMenuItemPart.Url);

            builder.Add(T[textContent], menu => menu.Url(url).LocalNav());
        }
        else if (menuItem.As<MenuItemsListPart>() is { } menuItemsListPart)
        {
            await builder.AddAsync(text, menu =>
                menuItemsListPart.MenuItems.AwaitEachAsync(child => AddAsync(menu, child)));
        }
    }

    private async Task AddContentMenuItemPartAsync(NavigationBuilder builder, LocalizedString text, IEnumerable<string> ids)
    {
        var contentItems = (await _contentManager.GetAsync(ids)).AsList();
        if (contentItems.Count == 1)
        {
            var contentItem = contentItems.Single();
            if (string.IsNullOrEmpty(text.Value)) text = GetTitle(contentItem);
            builder.Add(text, menu => UseDisplayUrl(menu, contentItem));
        }
        else
        {
            builder.Add(text, menu =>
            {
                foreach (var contentItem in contentItems)
                {
                    menu.Add(GetTitle(contentItem), child => UseDisplayUrl(child, contentItem));
                }
            });
        }
    }

    // This and UseDisplayUrl(), and the other changes to this class under
    // https://github.com/Lombiq/Orchard-Base-Theme/pull/74/files#diff-6dca81c4abae780b06f901d2ab84eb1e6d369e8d842da264a89199dfbaf11071
    // may be reverted once https://github.com/OrchardCMS/OrchardCore/issues/13943 is fixed by an Orchard Core upgrade.
    private string GetUrl(string contentPath) => _urlHelperLazy.Value.Content(contentPath);

    private void UseDisplayUrl(NavigationItemBuilder menu, IContent content) =>
        menu.Url(_urlHelperLazy.Value.DisplayContentItem(content));

    private static LocalizedString GetTitle(ContentItem contentItem) =>
        new(contentItem.DisplayText, contentItem.DisplayText);
}
