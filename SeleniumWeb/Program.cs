using Core.Providers;
using Core.Services;
using SeleniumWeb.Actions;

namespace SeleniumWeb;

public static class Program
{
    private static void Main()
    {
        var configurationProvider = new ConfigurationProvider();
        var crawlerActions = new CrawlerActions(configurationProvider);
        var resultsActions = new ResultsActions(configurationProvider, new ConsoleResultsWriter());

        var results = crawlerActions.Run();
        resultsActions.CalculateAndWriteResults(results);
    }
}