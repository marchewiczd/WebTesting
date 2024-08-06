using Core.Configuration;
using Core.Enums;
using Core.Factories;
using Core.Providers.Interfaces;
using FluentAssertions;
using NSubstitute;
using OpenQA.Selenium.Chrome;

namespace Core.Tests;

public class SeleniumDriverFactoryTests
{
    IConfigurationProvider _configProvider = Substitute.For<IConfigurationProvider>();

    [Fact]
    public void CreateDriver_ShouldReturnChromeDriver_WhenGivenChromeInput()
    {
        _configProvider.Get<SeleniumDriverOptions>().Returns(new SeleniumDriverOptions() { Headless = false });
        var webDriver = SeleniumDriverFactory.CreateDriver(WebDriverType.ChromeDriver, _configProvider);

        webDriver.Should().BeOfType(typeof(ChromeDriver));
        var browserName = (webDriver as ChromeDriver)?.Capabilities.GetCapability("browserName").ToString();
        browserName.Should().NotContain("headless");
        webDriver.Dispose();
    }

    [Fact]
    public void CreateDriver_ShouldReturnHeadlessChromeDriver_WhenGivenChromeInputWithOptions()
    {
        _configProvider.Get<SeleniumDriverOptions>().Returns(new SeleniumDriverOptions() { Headless = true });
        var webDriver = SeleniumDriverFactory.CreateDriver(WebDriverType.ChromeDriver, _configProvider);

        webDriver.Should().BeOfType(typeof(ChromeDriver));
        var browserName = (webDriver as ChromeDriver)?.Capabilities.GetCapability("browserName").ToString();
        browserName.Should().Contain("headless");
        webDriver.Dispose();
    }

    [Theory]
    [InlineData(WebDriverType.GeckoDriver)]
    [InlineData(WebDriverType.IeDriver)]
    public void CreateDriver_ShouldThrowException_WhenNotSupportedDriverIsChosen(WebDriverType driverType)
    {
        Action action = () => SeleniumDriverFactory.CreateDriver(driverType, _configProvider);
        action.Should().Throw<NotSupportedException>()
            .WithMessage($"{driverType.ToString()} is not supported.");
    }
}