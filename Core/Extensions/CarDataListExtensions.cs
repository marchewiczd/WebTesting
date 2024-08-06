using Core.Data;

namespace Core.Extensions;

public static class CarDataListExtensions
{
    public static double AverageDisplacementRounded(this List<CarDataModel> carDataList, int decimals = 2) =>
        Math.Round(carDataList.Sum(carData => carData.Displacement / 1000) / carDataList.Count, decimals);
}