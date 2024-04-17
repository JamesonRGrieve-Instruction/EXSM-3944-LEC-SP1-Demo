using NUnit.Framework;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using DemoMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace DemoMVC.Tests;

[TestFixture]
public class Test_Tests : PageTest
{
    [SetUp]
    public void Setup()
    {
        Controller HomeController = new HomeController();
    }

    [Test]
    public async Task TestGoogle()
    {       
        await Page.GotoAsync("https://google.ca");
        await Expect(Page).ToHaveTitleAsync("Google");
    }

}