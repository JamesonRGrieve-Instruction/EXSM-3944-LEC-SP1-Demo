namespace DemoMVCAuth.Tests.FrontEnd;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading;
public class PersonTest : PageTest
{
    [Test]
    public async Task Test1()
    {
        await using var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        { Headless = false });
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://localhost:7168/Person/Create");
        await page.FillAsync("input[name=FirstName]", "John");
        await page.FillAsync("input[name=LastName]", "Doe");
        await page.FillAsync("input[name=PhoneNumber]", "800-777-8888");
        await page.SelectOptionAsync("select[name=JobID]", "Bus Driver");
        await page.ClickAsync("input[type=submit][value=Create]");

        Thread.Sleep(30000);
    }
}