using NUnit.Framework;
using DemoMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace DemoMVC.Tests;

[TestFixture]
public class Test_Tests
{
    [SetUp]
    public void Setup()
    {
        Controller HomeController = new HomeController();
    }

    [Test]
    public void ThisShouldPass()
    {
        Assert.Pass();
    }

}