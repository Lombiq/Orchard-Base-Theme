using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    public static Task TestBaseThemeFeaturesAsync(this UITestContext context, bool skipLogin = false)
    {
        context.Get(By.Id("footer")).GetAttribute("class").Split().ShouldContain("text-center");
        context.TestZoneInsertion();
        return context.TestMainMenuWithAuthenticationAsync(skipLogin);
    }

    public static void TestZoneInsertion(this UITestContext context)
    {
        context.Get(By.ClassName("zoneInsertionExample_header"))
            .Text
            .Trim()
            .ShouldBe("Here you can easily inject shapes into the zones, kind of like always enabled widgets.");

        context.GetAllWhenOneExists(By.ClassName("zoneInsertionExample_footer"))
            .Count
            .ShouldBe(2);
    }

    public static async Task TestMainMenuWithAuthenticationAsync(this UITestContext context, bool skipLogin = false)
    {
        await context.ClickMainMenuPathAsync("Account", "Admin");
        context.SwitchToLastWindow();
        await context.DoWithRetriesOrFailAsync(() =>
            Task.FromResult(new Uri(context.Driver.Url).AbsolutePath == "/Admin"));
        context.SwitchToFirstWindow();

        await context.ClickMainMenuPathAsync("Account", "Log Out");

        if (skipLogin) return;

        await context.ClickMainMenuPathAsync("Log In");
        context.Exists(By.XPath("//form[@action = '/Login']/*[starts-with(name(), 'h') and contains(., 'Log in')]"));
    }

    public static async Task TestBaseThemeDependencyIsEnabledAsync(this UITestContext context)
    {
        await context.GoToAdminRelativeUrlAsync("/Features");
        context.Exists(By.Id("btn-disable-Lombiq_HelpfulExtensions_Widgets"));
    }

    public static void TestBlogRecipeMenuItemsAddedToMainMenu(this UITestContext context)
    {
        context.Get(By.CssSelector(".menuWidget__content .nav-link[href='/']")).Text.Trim().ShouldBe("Home");
        context.Get(By.CssSelector(".menuWidget__content .nav-link[href='/about']")).Text.Trim().ShouldBe("About");
    }

    public static async Task TestAddingMenuItemToBlogMainMenuAsync(this UITestContext context)
    {
        // The menu item has to be added through the admin by editing the menu like this because it can't be added
        // through a recipe. The setup recipe in OSOCE executes the Blog recipe which already creates a menu with the
        // "main-menu" alias. As it uses a random UUID for Content Item ID, it can't be updated from another recipe (if
        // attempted the setup will throw "ValidationException: Your alias is already in use." exception).
        // See https://github.com/Lombiq/Helpful-Libraries/issues/199 for a possible solution.
        await context.GoToAdminRelativeUrlAsync("/Contents/ContentItems/Menu");
        await context.ClickReliablyOnAsync(By.ClassName("edit"));

        await context.ClickReliablyOnAsync(By.XPath("//button[contains(., 'Add Menu Item')]"));
        await context.ClickReliablyOnAsync(By.XPath(
            "//div[contains(@class, 'card') and .//h4[contains(., 'Content Menu Item')]]//div[contains(@class, 'card-footer')]//a"));
        await context.ClickAndFillInWithRetriesAsync(By.Id("ContentMenuItemPart_Name"), "My Content");

        await context.SetContentPickerByDisplayTextAsync(
            "ContentMenuItemPart",
            "SelectedContentItem",
            "Man must explore, and this is exploration at its greatest");

        await context.ClickPublishAsync();
        await context.ClickPublishAsync();
        context.ShouldBeSuccess();

        await context.GoToHomePageAsync();
        context
            .Get(By.XPath("id('navigation')//li[contains(@class, 'menuWidget__topLevel')]/a[@href='/blog/post-1']"))
            .Text
            .Trim()
            .ShouldBe("My Content");
    }
}
