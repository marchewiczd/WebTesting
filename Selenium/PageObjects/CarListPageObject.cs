using Core.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Core.Extensions;
using Core.Data;
using Core.Providers.Interfaces;

namespace Selenium.PageObjects;

public class CarListPageObject(
    IWebDriver webDriver,
    ITranslationsProvider translationsProvider,
    SearchOptions searchOptions)
{
    private const string CookiesButtonId = "onetrust-accept-btn-handler";
    private const string CarMakeSelectXpath = "//select[@id=\"marka\"]";
    private const string CarModelSelectXpath = "//select[@id=\"model\"]";
    private const string SearchButtonXpath = "//button[@class=\"oglSearchbar__btn btn btn--orange\"]";
    private const string CarListItemXpath = "//div[contains(@class, \"list--item--withPrice\")]";
    private const string CarPriceXpath = ".//p[@class=\"list__item__price__value\"]";
    private const string CarYearXpath =
        ".//li[contains(@class, \"details--icons--element--rok_produkcji\")]/div[1]/p[2]";
    private const string CarMileageXpath =
        ".//li[contains(@class, \"details--icons--element--przebieg\")]/div[1]/p[2]";
    private const string CarEngineCapacityXpath =
        ".//li[contains(@class, \"details--icons--element--pojemnosc\")]/div[1]/p[2]";

    private string NextPageXpath =>
        $"//a[@title=\"{translationsProvider.Get("car_list.next_page_button")}\"]";

    public void GoToPage() =>
        webDriver.Navigate().GoToUrl(searchOptions.Uri);

    public void AcceptCookies() =>
        webDriver.FindElement(By.Id(CookiesButtonId)).Click();

    public void InputSearchPhase(string searchPhrase)
    {
        const int carMakeIndex = 0;
        const int carModelIndex = 1;
        var carInfo = searchPhrase.Split(" ");

        SelectElement(CarMakeSelectXpath, carInfo[carMakeIndex]);
        SelectElement(CarModelSelectXpath, carInfo[carModelIndex]);
    }

    public void PressSearchButton() =>
        webDriver.FindAndClick(By.XPath(SearchButtonXpath));

    public IEnumerable<CarDataModel> GetCarsFromPages(int numberOfPages = 0)
    {
        var results = new List<CarDataModel>();

        if (numberOfPages == 0)
        {
            results.AddRange(GetCarsFromPage());

            while(NextPageExists())
            {
                GoToNextPage();
                results.AddRange(GetCarsFromPage());
            }

            return results;
        }

        for (var i = 0; i < numberOfPages; i++)
        {
            results.AddRange(GetCarsFromPage());

            if (!NextPageExists())
            {
                return results;
            }

            GoToNextPage();
        }

        return results;
    }

    private bool NextPageExists()
    {
        try
        {
            webDriver.FindElement(By.XPath(NextPageXpath));
        }
        catch (NoSuchElementException)
        {
            return false;
        }

        return true;
    }

    private IEnumerable<CarDataModel> GetCarsFromPage()
    {
        var carElements = webDriver.FindElements(By.XPath(CarListItemXpath));

        foreach (var carElement in carElements)
        {
            var priceElement = carElement.FindElementIfExists(By.XPath(CarPriceXpath));
            var parseResult = double.TryParse(priceElement?.Text.NormalizePriceText(), out var price);

            yield return new CarDataModel()
            {
                Price = parseResult ? price : 0,
                ProductionYear = carElement.FindElementIfExists(By.XPath(CarYearXpath)).GetTextAsDouble(),
                Mileage = carElement.FindElementIfExists(By.XPath(CarMileageXpath)).GetTextAsDouble(),
                Displacement = carElement.FindElementIfExists(By.XPath(CarEngineCapacityXpath)).GetTextAsDouble()
            };
        }
    }

    private void GoToNextPage()
    {
        if (!NextPageExists())
        {
            return;
        }

        webDriver.FindAndClick(By.XPath(NextPageXpath));
    }

    private void SelectElement(string xPath, string optionName)
    {
        var selectElement = new SelectElement(webDriver.FindElement(By.XPath(xPath)));

        if (!selectElement.GetOptionsText().Contains(optionName))
            throw new ArgumentException($"{optionName} doesn't exists.");

        selectElement.SelectByText(optionName);
    }
}