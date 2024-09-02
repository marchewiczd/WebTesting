using Microsoft.Playwright;

namespace Playwright.PageObject;

public class MainPage(IPage page)
{
    public const string Title = "BrickLink - Buy and sell LEGO Parts, Sets and Minifigures";
    
    public async Task NavigateAsync()
    {
        var url = TestContext.Parameters["URL"];
        if (url == null)
            throw new NullReferenceException("URL is not specified in test context.");

        await page.GotoAsync(url);
    }
}