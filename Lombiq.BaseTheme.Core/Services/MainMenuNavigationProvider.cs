using Lombiq.HelpfulLibraries.Common.Utilities;
using Lombiq.HelpfulLibraries.OrchardCore.Navigation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Extensions;
using OrchardCore.Menu.Models;
using OrchardCore.Mvc.Utilities;
using OrchardCore.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Core.Services;

public class MainMenuNavigationProvider : MainMenuNavigationProviderBase
{
    private readonly IContentHandleManager _contentHandleManager;
    private readonly IContentManager _contentManager;
    private readonly IUrlHelperFactory _urlHelperFactory;

    public MainMenuNavigationProvider(
        IHttpContextAccessor hca,
        IStringLocalizer<MainMenuNavigationProvider> stringLocalizer,
        IContentHandleManager contentHandleManager,
        IContentManager contentManager,
        IUrlHelperFactory urlHelperFactory)
        : base(hca, stringLocalizer)
    {
        _contentHandleManager = contentHandleManager;
        _contentManager = contentManager;
        _urlHelperFactory = urlHelperFactory;
    }

    protected override async Task BuildAsync(NavigationBuilder builder)
    {
        if (await _contentHandleManager.GetContentItemIdAsync("alias:main-menu") is not { } id ||
            await _contentManager.GetAsync(id) is not { } contentItem ||
            !contentItem.TryGet<MenuItemsListPart>(out var menuItemsListPart))
        {
            return;
        }

        foreach (var menuItem in menuItemsListPart.MenuItems)
        {
            await AddAsync(builder, menuItem);
        }
    }

    private Task AddAsync(NavigationBuilder builder, ContentItem menuItem)
    {
        if (menuItem.TryGet<HtmlMenuItemPart>(out var htmlMenuItemPart))
        {
            var textContent = HtmlHelper.ConvertToPlainText(htmlMenuItemPart.Html);
            builder.Add(new(textContent, textContent), menu => menu
                .Url("#")
                .LocalNav()
                .AddClass(PascalCaseClassify("menuItem__Html_", textContent)));
            return Task.CompletedTask;
        }

        var text = GetTitle(menuItem);

        return menuItem.TryGet<LinkMenuItemPart>(out var linkMenuItemPart)
            ? Task.FromResult(
                builder.Add(text, menu => menu
                    .Url(linkMenuItemPart.Url)
                    .Local(linkMenuItemPart.Target != "_blank")
                    .AddClass(PascalCaseClassify("menuItem__Text_", text.Name))))
            : AddInnerAsync(builder, menuItem, text);
    }

    private async Task AddInnerAsync(NavigationBuilder builder, ContentItem menuItem, LocalizedString text)
    {
        if (menuItem.TryGet<ContentMenuItemPart>(out var contentMenuItemPart))
        {
            if (_hca.HttpContext is not { } httpContext ||
                contentMenuItemPart.GetProperty<IEnumerable<string>>("SelectedContentItem.ContentItemIds") is not { } ids ||
                (await _contentManager.GetAsync(ids))?.AsList() is not { Count: > 0 } contentItems)
            {
                return;
            }

            var urlHelper = _urlHelperFactory.GetUrlHelper(await httpContext.GetActionContextAsync());
            if (contentItems.Count == 1)
            {
                AddContentItem(builder, urlHelper, contentItems.Single(), text);
                return;
            }

            builder.Add(text, menu =>
            {
                foreach (var contentItem in contentItems)
                {
                    AddContentItem(menu, urlHelper, contentItem, text: null);
                }
            });
        }
        else if (menuItem.TryGet<MenuItemsListPart>(out var menuItemsListPart))
        {
            await builder.AddAsync(text, menu =>
                menuItemsListPart.MenuItems.AwaitEachAsync(child => AddAsync(menu, child)));
        }
    }

    private static void AddContentItem(NavigationBuilder builder, IUrlHelper urlHelper, ContentItem contentItem, LocalizedString text)
    {
        if (string.IsNullOrEmpty(text?.Value)) text = GetTitle(contentItem);

        builder.Add(text, menu => menu
            .Url(urlHelper.DisplayContentItem(contentItem))
            .AddClass(PascalCaseClassify("menuItem__ContentText_", text.Name)));
    }

    private static LocalizedString GetTitle(ContentItem contentItem)
    {
        var displayText = contentItem.DisplayText ?? string.Empty;
        return new LocalizedString(displayText, displayText);
    }

    private static string PascalCaseClassify(string prefix, string text) =>
        prefix + text.Trim().HtmlClassify().ToPascalCase('-');
}
