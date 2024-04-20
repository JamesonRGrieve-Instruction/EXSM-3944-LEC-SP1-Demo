using NUnit.Framework;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Threading.Tasks;
namespace DemoMVC.Tests;

[TestFixture]
public class Test_Tests : PageTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestGoogle()
    {
        await Page.GotoAsync("https://localhost:7121/home/sample");
        await Expect(Page).ToHaveTitleAsync("Razor Sample - DemoMVC");
        await Task.Delay(3000);
        await Page.FillAsync("input[name=FirstName]", "John");
        await Task.Delay(2000);
    }

}