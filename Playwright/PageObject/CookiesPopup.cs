using Microsoft.Playwright;

namespace Playwright.PageObject;

public partial class CookiesPopup(IPage page)
{
    private ILocator CookiesJustNecessaryButton => page.GetByRole(AriaRole.Button, 
        new() { NameString = "Just necessary" });
    
    private ILocator CookiesAcceptAllButton => page.GetByRole(AriaRole.Button, 
        new() { NameString = "Accept all cookies" });
    
    private ILocator CookiesContainer => page.GetByText(YourCookieSettingsRegex()).Nth(1);

    public async Task<bool> IsVisible() => 
        await CookiesContainer.IsVisibleAsync();

    public async Task AcceptAllCookies() => 
        await CookiesAcceptAllButton.ClickAsync();

    public async Task AcceptJustNecessaryCookies() => 
        await CookiesJustNecessaryButton.ClickAsync();
    
    [GeneratedRegex("Your Cookie Settings", 
        RegexOptions.IgnoreCase)]
    private static partial Regex YourCookieSettingsRegex();
}