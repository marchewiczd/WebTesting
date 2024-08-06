using Core.Data;
using Core.Extensions;
using FluentAssertions;

namespace Core.Tests;

public class EnumerableExtensionsTests
{
    private readonly List<CarDataModel> _carDataList = new()
    {
        new CarDataModel
        {
            Displacement = 2000,
            Mileage = 230_000,
            Price = 25_000,
            ProductionYear = 2012
        },
        new CarDataModel
        {
            Displacement = 1000,
            Mileage = 1000,
            Price = 75_000,
            ProductionYear = 2021
        },
    };

    [Fact]
    public void CalculateAverageRounded_ShouldCalculateAverageRounded()
    {
        const int decimals = 3;
        var expectedValue =
            Math.Round((_carDataList[0].Mileage + _carDataList[1].Mileage) / 2, decimals);

        _carDataList.AverageRounded(data => data.Mileage).Should().Be(expectedValue);
    }

    [Fact]
    public void CalculateAverageRounded_ThrowsNull_WhenSourceIsNull()
    {
        IEnumerable<CarDataModel>? source = null;
        Action action = () => source!.AverageRounded(data => data.ProductionYear);

        action.Should().Throw<ArgumentNullException>();
    }
}