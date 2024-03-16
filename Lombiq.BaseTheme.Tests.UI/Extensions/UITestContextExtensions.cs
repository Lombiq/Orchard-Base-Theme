using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lombiq.BaseTheme.Tests.UI.Extensions;

public static class UITestContextExtensions
{
    public static async Task ClickMainMenuPathAsync(this UITestContext context, string topMenuLabel, string subMenuLabel = null)
    {
        var byFirst = By.XPath(
            $"//div[contains(@class, \"menuWidget__content\")]/ul/li/a" +
            $"[contains(@class, \"nav-link\") and contains(., {JsonSerializer.Serialize(topMenuLabel)})]");

        if (string.IsNullOrWhiteSpace(subMenuLabel))
        {
            await context.ClickReliablyOnAsync(byFirst);
        }
        else
        {
            await context.SelectFromBootstrapDropdownReliablyAsync(
                context.Get(byFirst),
                By.XPath($".//*[contains(@class, 'dropdown-item') and contains(., {JsonSerializer.Serialize(subMenuLabel)})]"));
        }
    }
}
