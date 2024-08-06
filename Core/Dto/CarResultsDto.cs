using Newtonsoft.Json;

namespace Core.Dto;

public record CarResultsDto
{
    public string? SearchPhrase { get; init; }
    public int NumberOfItems { get; init; }
    public double AveragePrice { get; init; }
    public double AverageMileage { get; init; }
    public double AverageDisplacement { get; init; }
    public double AverageYearOfProduction { get; init; }

    public override string ToString() =>
        JsonConvert.SerializeObject(this);
}