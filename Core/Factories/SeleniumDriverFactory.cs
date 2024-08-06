using Core.Configuration;
using Core.Enums;
using Core.Providers.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.Factories;

public static class SeleniumDriverFactory
{
    public static IWebDriver CreateDriver(WebDriverType webDriverType, IConfigurationProvider configurationProvider) =>
        webDriverType switch
        {
            WebDriverType.ChromeDriver => InitChromeDriver(configurationProvider),
            _ => throw new NotSupportedException($"{webDriverType.ToString()} is not supported.")
        };

    private static ChromeDriver InitChromeDriver(IConfigurationProvider configurationProvider)
    {
        var chromeOptions = new ChromeOptions();
        var webDriverOptions = configurationProvider.Get<SeleniumDriverOptions>();

        if (webDriverOptions is not null && webDriverOptions.Headless)
            chromeOptions.AddArgument("--headless");

        var webDriver = new ChromeDriver(chromeOptions);
        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        return webDriver;
    }
}