using Playwright.Attributes;
using Playwright.PageObject;

namespace Playwright.Tests;

[TestFixture]
public class MainPageTests : PageTest
{
    private MainPage _mainPage;
    private CookiesPopup _cookiesPopup;

    [SetUp]
    public void Setup()
    { 
        _mainPage = new MainPage(Page);
        _cookiesPopup = new CookiesPopup(Page);
    } 

    [Test]
    [Critical, SingleFunctionality]
    public async Task MainPageCanBeReached()
    {
        await _mainPage.NavigateAsync();
        await Expect(Page).ToHaveTitleAsync(MainPage.Title);
    }

    [Test]
    [SingleFunctionality]
    public async Task VerifyVisibilityAndAcceptCookies()
    {
        await _mainPage.NavigateAsync();
        Assert.That(await _cookiesPopup.IsVisible(), Is.True);
        await _cookiesPopup.AcceptJustNecessaryCookies();
        Assert.That(await _cookiesPopup.IsVisible(), Is.False);
    }
}