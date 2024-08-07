using Core.Configuration;
using Core.Data;
using Core.Dto;
using Core.Extensions;
using Core.Providers.Interfaces;
using Core.Services.Interfaces;

namespace Selenium.Actions;

public class ResultsActions(IConfigurationProvider configurationProvider, IResultsWriter resultsWriter)
{
    public void CalculateAndWriteResults(List<CarDataModel> carDataList)
    {
        var searchOptions = configurationProvider.Get<SearchOptions>();

        var avgCarData = new CarResultsDto
        {
            SearchPhrase = searchOptions?.SearchPhrase,
            NumberOfItems = carDataList.Count,
            AveragePrice = carDataList.AverageRounded(data => data.Price),
            AverageMileage = carDataList.AverageRounded(data => data.Mileage),
            AverageDisplacement = carDataList.AverageDisplacementRounded(),
            AverageYearOfProduction = carDataList.AverageRounded(data => data.ProductionYear)
        };

        resultsWriter.WriteResults(avgCarData);
    }
}