using Core.Configuration;
using Core.Data;
using Core.Enums;
using Core.Factories;
using Core.Providers;
using Core.Providers.Interfaces;
using Selenium.PageObjects;

namespace Selenium.Actions;

public class CrawlerActions(IConfigurationProvider configurationProvider)
{
    public List<CarDataModel> Run()
    {
        var searchOptions = configurationProvider.Get<SearchOptions>();
        ArgumentNullException.ThrowIfNull(searchOptions);

        using var webDriver = SeleniumDriverFactory.CreateDriver(WebDriverType.ChromeDriver, configurationProvider);
        var carListPage =
            new CarListPageObject(webDriver, new TranslationsProvider(configurationProvider), searchOptions);

        carListPage.GoToPage();
        carListPage.AcceptCookies();
        carListPage.InputSearchPhase(searchOptions.SearchPhrase);
        carListPage.PressSearchButton();

        var carDataList = carListPage.GetCarsFromPages(searchOptions.NumberOfPagesRequested).ToList();

        return carDataList;
    }
}