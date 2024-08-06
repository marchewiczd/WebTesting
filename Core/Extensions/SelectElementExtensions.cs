using OpenQA.Selenium.Support.UI;

namespace Core.Extensions;

public static class SelectElementExtensions
{
    public static IEnumerable<string> GetOptionsText(this SelectElement selectElement) =>
        selectElement.Options.Select(option => option.Text);
}