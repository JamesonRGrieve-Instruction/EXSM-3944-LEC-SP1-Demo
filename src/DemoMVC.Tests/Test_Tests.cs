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
        await Page.GotoAsync("http://localhost:5203");
        await Expect(Page).ToHaveTitleAsync("Home Page - DemoMVC");
    }

}