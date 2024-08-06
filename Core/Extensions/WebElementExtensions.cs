using OpenQA.Selenium;

namespace Core.Extensions;

public static class WebElementExtensions
{
    public static IWebElement? FindElementIfExists(this IWebElement webElement, By? by)
    {
        try
        {
            return webElement.FindElement(by);
        }
        catch (NoSuchElementException)
        {
            return null;
        }
    }

    public static int GetTextAsInt(this IWebElement? webElement) =>
        webElement is not null && int.TryParse(webElement.Text, out var result)
            ? result : -1;

    public static double GetTextAsDouble(this IWebElement? webElement) =>
        webElement is not null && double.TryParse(webElement.Text, out var result)
            ? result : -1;
}