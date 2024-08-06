using OpenQA.Selenium;

namespace Core.Extensions;

public static class SeleniumDriverExtensions
{
    public static void ExecuteJavaScript(this IWebDriver driver, string script, params object[] args) =>
        (driver as IJavaScriptExecutor)?.ExecuteScript(script, args);

    public static void ScrollIntoView(this IWebDriver driver, IWebElement element) =>
        driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", element);

    public static void FindAndClick(this IWebDriver driver, By by)
    {
        var nextPageElement = driver.FindElement(by);
        driver.ScrollIntoView(nextPageElement);
        nextPageElement.Click();
    }
}