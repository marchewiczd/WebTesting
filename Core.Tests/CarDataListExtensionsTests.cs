using FluentAssertions;
using Core.Data;
using Core.Extensions;

namespace Core.Tests;

public class CarDataListExtensionsTests
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
    public void CalculateAvgDisplacement_ShouldCalculateAverageDisplacement_WhenGivenCorrectInput()
    {
        const int displacementDigits = 5;
        var expectedDisplacement =
            Math.Round((double)(_carDataList[0].Displacement + _carDataList[1].Displacement) / 1000 / 2, displacementDigits);

        _carDataList.AverageDisplacementRounded().Should().Be(expectedDisplacement);
    }
}