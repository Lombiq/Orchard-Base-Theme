using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    public static Task TestBaseThemeFeaturesAsync(this UITestContext context)
    {
        context.Get(By.Id("footer")).GetAttribute("class").Split().ShouldContain("text-center");
        context.TestZoneInsertion();
        return context.TestMainMenuWithAuthenticationAsync();
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

    public static async Task TestMainMenuWithAuthenticationAsync(this UITestContext context)
    {
        await context.ClickMainMenuPathAsync("Account", "Admin");
        context.SwitchToLastWindow();
        await context.DoWithRetriesOrFailAsync(() =>
            Task.FromResult(new Uri(context.Driver.Url).AbsolutePath == "/Admin"));
        context.SwitchToFirstWindow();

        await context.ClickMainMenuPathAsync("Account", "Log Out");

        await context.ClickMainMenuPathAsync("Log In");
        context.Exists(By.XPath("//form[@action = '/Login']/*[starts-with(name(), 'H') and contains(., 'Log in')]"));
    }
}
